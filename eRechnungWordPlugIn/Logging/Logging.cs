using Logging.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace LogService
{
    public static class Log
    {

        public enum LogPriority
        {
            Low = 0,
            Medium,
            High
        }
#if DEBUG || TRACE
        private static bool _traceEnabled = true;
#else
        private static bool _traceEnabled = false; 
#endif
        private static int _numLogFiles = 20;
        private const string _logFileName = "eRechnungLog";
        private static string _logfn;
        private static void GetLogFn()
        {
            string logDir = @"eRechnung\Log";
            string logfn;
            string docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            logDir = Path.Combine(docs, logDir);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
            else
            {
                LogCleanup(logDir);
            }
            logfn = System.IO.Path.Combine(logDir, _logFileName + DateTime.Now.ToString("yyyy-MM-dd-HHmmssff") + ".log");
            _logfn = logfn;
        }

        private static void LogCleanup(string logDir)
        {
            string pattern = Path.Combine(_logFileName + "*") + ".log";
            List<string> entries = new List<string>(Directory.GetFiles(logDir, pattern));
            if (entries.Count() > _numLogFiles)
            {
                Debug.WriteLine("Anzahl Files:{0}", entries.Count());
                List<string> toDelete = entries.OrderBy(s => s).Take(entries.Count - _numLogFiles).ToList();
                foreach (string delFile in toDelete)
                {
                    Debug.WriteLine(string.Format("File to delete: {0}", delFile));
                    File.Delete(delFile);
                }
            }
        }
        public static void LogWrite(CallerInfo cInfo, LogPriority level, string format, params object[] parms)
        {
            if ((int)level < Settings.Default.LogPriority)
                return;
            WriteToLog(level.ToString(), string.Format(format, parms), cInfo);

        }
        private static Mutex _semaphore = new Mutex();
        public static void TraceWrite(CallerInfo cInfo, string format, params object[] parms)
        {
            if (!_traceEnabled) return;
            WriteToLog("Trace", string.Format(format, parms), cInfo);
        }
        public static void TraceWrite(CallerInfo cInfo, string message)
        {
            if (!_traceEnabled) return;
            WriteToLog("Trace", message, cInfo);
        }
        private static void WriteToLog(string level, string message, CallerInfo cInfo)
        {
            string msg = "";
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ff\t") + level + "\t" + cInfo.CallerFilePath + "\t" + cInfo.CallerMemberName + "\t" + cInfo.CallerLineNumber + "\t" + message;
            if (_semaphore.WaitOne(100))
            {
                if (string.IsNullOrEmpty(_logfn))
                {
                    GetLogFn();
                    string msg1 = "Timestamp\tType\tFile\tMethod\tLine\tMessage";
                    File.AppendAllLines(_logfn, new List<string> { msg1 });
                    Assembly asm = Assembly.GetExecutingAssembly();
                    var version = asm.GetName().Version;
                    string initStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ff\t");
                    var initstrings = new List<string>{
                        initStamp+"Init\tLog Init\t******************************",
                        initStamp+$"Init\tLog Init\teRechnung Word PlugIn Version {version.ToString()}",
                        initStamp+"Init\tLog Init\t******************************",

                        };
                    File.AppendAllLines(_logfn, initstrings);
                }
                File.AppendAllLines(_logfn, new List<string> { msg });
                _semaphore.ReleaseMutex();
            }
            else
            {
                // Silently skip
                Debug.WriteLine("Semaphore not acquired: '" + msg + "'");
            }
            return;
        }

        public static void SetLogLevel(LogPriority level)
        {
            Settings.Default.LogPriority = (int)level;
            Settings.Default.Save();
        }

        public static void EnableTrace()
        {
            _traceEnabled = true;
        }

        public static void DisableTrace()
        {
            _traceEnabled = false;
        }

        public static LogPriority GetLogLevel
        {
            get { return (LogPriority)Settings.Default.LogPriority; }
        }

    }
    public class CallerInfo
    {
        public string CallerFilePath { get; private set; }

        public string CallerMemberName { get; private set; }

        public int CallerLineNumber { get; private set; }

        private CallerInfo(string callerFilePath, string callerMemberName, int callerLineNumber)
        {
            this.CallerFilePath = callerFilePath;
            this.CallerMemberName = callerMemberName;
            this.CallerLineNumber = callerLineNumber;
        }

        public static CallerInfo Create(
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            return new CallerInfo(Path.GetFileName(callerFilePath), callerMemberName, callerLineNumber);
        }
    }
}
