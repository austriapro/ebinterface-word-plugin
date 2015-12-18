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
                logCleanup(logDir);
            }
            logfn = System.IO.Path.Combine(logDir, _logFileName + DateTime.Now.ToString("yyyy-MM-dd-HHmmssff") + ".log");
            _logfn = logfn;
        }

        private static void logCleanup(string logDir)
        {
            string pattern = Path.Combine(_logFileName + "*") + ".log";
            List<string> entries = new List<string>(Directory.GetFiles(logDir,pattern));
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
        public static void LogWrite(LogPriority level, string format, params object[] parms)
        {
            if ((int)level < Settings.Default.LogPriority)
                return;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            WriteToLog(level.ToString(), string.Format(format, parms), methodBase, GetCaller(stackTrace));

        }
        private static Mutex _semaphore = new Mutex();
        public static void TraceWrite(string format, params object[] parms)
        {
            if (!_traceEnabled) return;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            WriteToLog("Trace", string.Format(format, parms), methodBase, GetCaller(stackTrace));
        }
        public static void TraceWrite(string message)
        {
            if (!_traceEnabled) return;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            WriteToLog("Trace", message, methodBase, GetCaller(stackTrace));
        }
        private static string GetCaller(StackTrace stack)
        {
            if (stack.GetFrames().Count() > 2)
            {
                MethodBase methodBase = stack.GetFrame(2).GetMethod();
                string caller = methodBase.DeclaringType.FullName + "." + methodBase.Name;
                return caller;
            }
            return "-unknown-";
        }

        private static void WriteToLog(string level, string message, MethodBase methodBase, string caller)
            {
                string msg = "";
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ff\t") + level + "\t" + methodBase.DeclaringType.FullName + "\t" + methodBase.Name + "\t" + caller + "\t" + message;
            if (_semaphore.WaitOne(100))
            {
                if (string.IsNullOrEmpty(_logfn))
                {
                    GetLogFn();
                    string msg1 = "Timestamp\tType\tDeclaring Type\tMethod\tCaller\tMessage";
                    File.AppendAllLines(_logfn, new List<string> { msg1 });
                }
                File.AppendAllLines(_logfn, new List<string> { msg });
                _semaphore.ReleaseMutex();
            }
            else
            {
                // Silently skip
                Debug.WriteLine("Semaphore ot acquired: '" + msg + "'");
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
}
