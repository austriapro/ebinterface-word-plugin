using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using SimpleEventBroker;

namespace ebIServices.StartProcess
{
    public class StartProcessDienst : IStartProcessDienst
    {
        private readonly IUnityContainer _uc;
        public StartProcessDienst(IUnityContainer uc)
        {
            _uc = uc;
        }
        public string Arguments { get;  set; }
        public string Executable { get;  set; }
        public string WorkingDirectory { get;  set; }
        public List<string> Messages { get; private set; }
        public List<string> ErrorMessages { get; private set; }

 
        public event EventHandler ProcessFinishedEvent;
        private void ProcessFinishedFire()
        {
            EventHandler handler = ProcessFinishedEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool Run(string exeFile, string arg, string workDir, bool noWindow)
        {
            Messages = new List<string>();
            ErrorMessages = new List<string>();

            if (!File.Exists(exeFile))
            {
                ErrorMessages.Add("PS00084 '" + exeFile + "' wurde nicht gefunden.");
                return false;
            }
            if (!Directory.Exists(workDir))
            {
                ErrorMessages.Add("PS00085 Das Verzeichnis '" + workDir + "' wurde nicht gefunden.");
                return false;
            }

            Arguments = arg;
            Executable = exeFile;
            WorkingDirectory = workDir;
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = Arguments,
                FileName = Executable,
                WorkingDirectory = WorkingDirectory,
                CreateNoWindow = noWindow,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            Process proc = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };
            Action<object, DataReceivedEventArgs> writeOutData = (sender, e) => Messages.Add(e.Data);
            Action<object, DataReceivedEventArgs> writeErrData = (sender, e) => ErrorMessages.Add(e.Data);
            proc.ErrorDataReceived += (sender, e) => writeErrData(sender, e);
            proc.OutputDataReceived += (sender, e) => writeOutData(sender, e);
            proc.Exited += proc_Exited;
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            return true;
        }

        void proc_Exited(object sender, EventArgs e)
        {
            ProcessFinishedFire();
        }
        private const int SHGFI_EXETYPE = 0x000002000;
        private const int IMAGE_NT_SIGNATURE = 0x00004550;
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            private IntPtr hIcon;
            private IntPtr iIcon;
            private uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            private string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            private string szTypeName;
        }; 

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFILEINFO psfi, int cbFileInfo, int uFlags);

        public static bool IsConsoleApplication(string path)
        {
            SHFILEINFO fi;
            int exeType = SHGetFileInfo(path, 0, out fi, 0, SHGFI_EXETYPE);
            return (LoWord(exeType) == IMAGE_NT_SIGNATURE && HiWord(exeType) == 0);
        }
        public static int LoWord(int dwValue)
        {
            return dwValue & 0xFFFF;
        }
        public static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xFFFF;
        }
    }
}