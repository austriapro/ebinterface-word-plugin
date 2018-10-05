using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIServices.StartProcess;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SimpleEventBroker;

namespace ebIServices.StartProcess.Tests
{
    [TestFixture]
    public class StartProcessServiceTests : Common
    {
        private const string ExeFileName = @"..\..\..\ZustellDienstSample\bin\Debug\ZustellDienstSample.exe";
        private const string WindowExeFileName = @"..\..\..\ZustellDienstWindowSample\bin\Debug\ZustellDienstWindowSample.exe";
        private ManualResetEvent _waitEvent;
        private const string arg = @"""C:\TFS\ebInterface\Codeplex\Dvt4p1\eRechnung\ebIServicesTests\bin\Debug\Daten\Rechng-2014-500.xml"" ";
        [Test]
        public void StartProcessServiceTestOk()
        {
            var sProc = UContainer.Resolve<IStartProcessDienst>();
            Assert.IsNotNull(sProc);
            sProc.ProcessFinishedEvent += OnStartProcessDienstProcessFinished;
            _waitEvent = new ManualResetEvent(false);
            sProc.Run(ExeFileName, arg, @"C:\TFS\ebInterface\Codeplex\Dvt4p1\eRechnung\ebIServicesTests\bin\Debug\Daten\",true);
            _waitEvent.WaitOne(2 * 1000); // Nötig, damit der Consolen-Output im Testergebnis erscheint
        }

        [Test]
        public void StartProcessServiceWindowTestOk()
        {
            var sProc = UContainer.Resolve<IStartProcessDienst>();
            Assert.IsNotNull(sProc);
            sProc.ProcessFinishedEvent += OnStartProcessDienstProcessFinished;
            _waitEvent = new ManualResetEvent(false);
            sProc.Run(WindowExeFileName, arg, @"C:\TFS\ebInterface\Codeplex\Dvt4p1\eRechnung\ebIServicesTests\bin\Debug\Daten\", true);
            _waitEvent.WaitOne(2 * 1000); // Nötig, damit der Consolen-Output im Testergebnis erscheint
        }

        public void OnStartProcessDienstProcessFinished(object sender, EventArgs args)
        {
            //  StartProcessDienst.ProcessFinishedEventArgs arg = args as  StartProcessDienst.ProcessFinishedEventArgs;
            StartProcessDienst sProc = (StartProcessDienst)sender;
            foreach (string message in sProc.Messages)
            {
                Console.WriteLine(message);
            }
            foreach (string message in sProc.ErrorMessages)
            {
                Console.WriteLine(message);
            }
            _waitEvent.Set();
        }
        [Test]
        public void RunTest()
        {

        }
    }
}
