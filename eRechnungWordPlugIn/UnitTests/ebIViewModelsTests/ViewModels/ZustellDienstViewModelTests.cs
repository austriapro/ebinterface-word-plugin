using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsManager;

namespace ebIViewModelsTests.ViewModels
{
    [TestClass]
    public class ZustellDienstViewModelTests : CommonTestSetup
    {
        private const string ZustellSaveFile = @"Daten\ZustellSaveInvoice.xml";
        private const string ExeFileName = @"..\..\..\..\ZustellDienstSample\bin\Debug\ZustellDienstSample.exe";

        [TestMethod]
        public void RunZustellDienstTestOk()
        {
            PlugInSettings.Default.DeliveryExePath = ExeFileName;
            PlugInSettings.Default.DeliveryWorkDir = Path.GetFullPath(Path.GetDirectoryName(ZustellSaveFile)); // Path.GetPathRoot(ZustellSaveFile);
            PlugInSettings.Default.DeliveryArgs = "{0}";
            InvVm.SaveEbinterfaceCommand.Execute(ZustellSaveFile);
            InvVm.WaitForProcess = true;
            string fn = Path.GetFullPath(ZustellSaveFile);
            InvVm.RunZustellDienstButton.Execute(fn);
            Assert.IsTrue(InvVm.ProcessMessages.Count>0);
            foreach (string message in InvVm.ProcessMessages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
