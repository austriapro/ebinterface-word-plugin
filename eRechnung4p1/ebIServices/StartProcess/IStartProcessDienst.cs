using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using SimpleEventBroker;

namespace ebIServices.StartProcess
{
    public interface IStartProcessDienst
    {
        string Arguments { get; set; }
        string Executable { get; set;  }
        string WorkingDirectory { get; set; }
        List<string> Messages { get; }
        List<string> ErrorMessages { get; }
        
        event EventHandler ProcessFinishedEvent;

        bool Run(string exeFile, string arg, string workDir,bool noWindow);
    }
}