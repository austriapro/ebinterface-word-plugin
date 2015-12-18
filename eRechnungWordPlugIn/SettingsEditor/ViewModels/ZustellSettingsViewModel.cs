using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Practices.Unity;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;

namespace SettingsEditor.ViewModels
{
    public class ZustellSettingsViewModel : ViewModelBase
    {
        const string BillerMail = "/eb:Invoice/eb:Biller/eb:Address/eb:Email";
        const string ReceipientMail = "/eb:Invoice/eb:Receipient/eb:Address/eb:Email";

        private readonly IUnityContainer _uc;
        private IOpenFileDialog _openFile;
        private IFolderBrowserDialog _folderBrowser;
        public ZustellSettingsViewModel(IUnityContainer uc, IDialogService dialog, IOpenFileDialog openFileDialog, IFolderBrowserDialog folderBrowser)
        {
            _uc = uc;
            _dlg = dialog;
            _openFile = openFileDialog;
            _folderBrowser = folderBrowser;
            _exeFileName = PlugInSettings.Default.DeliveryExePath;
            _arguments = PlugInSettings.Default.DeliveryArgs;
            _workingDirectory = PlugInSettings.Default.DeliveryWorkDir;
        }

        #region Properties

        private string _exeFileName;
        public string ExeFileName
        {
            get { return _exeFileName; }
            set
            {
                if (_exeFileName == value)
                    return;
                _exeFileName = value;
                OnPropertyChanged();
            }
        }
        private string _arguments;
        public string Arguments
        {
            get { return _arguments; }
            set
            {
                if (_arguments == value)
                    return;
                _arguments = value;
                OnPropertyChanged();
            }
        }
        private string _workingDirectory;
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set
            {
                if (_workingDirectory == value)
                    return;
                _workingDirectory = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Speichern Command

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => SaveClick());
                return _saveCommand;
            }
        }

        private void SaveClick()
        {
            PlugInSettings.Default.DeliveryArgs = Arguments;
            PlugInSettings.Default.DeliveryExePath = ExeFileName;
            PlugInSettings.Default.DeliveryWorkDir = Path.GetFullPath(WorkingDirectory);
            PlugInSettings.Default.Save();
        }
        #endregion
        #region GetFile Button

        private ICommand _getExeFileCommand;
        public ICommand GetExeFileCommand
        {
            get
            {
                _getExeFileCommand = _getExeFileCommand ?? new RelayCommand(param => GetExeFileClick());
                return _getExeFileCommand;
            }
        }

        private void GetExeFileClick()
        {
            _openFile.InitialDirectory = Environment.SpecialFolder.ProgramFiles.ToString();
            _openFile.Multiselect = false;
            var rc = _dlg.ShowOpenFileDialog(_openFile);
            if (rc == DialogResult.OK)
            {
                if (!File.Exists(_openFile.FileName))
                {
                    _dlg.ShowMessageBox("Datei " + _openFile.FileName + " wurde nicht gefunden.", "Zustelldienst", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ExeFileName = _openFile.FileName;
            }
        }
        #endregion
        #region Working Directory Button

        private ICommand _workingDirCommand;
        public ICommand WorkingDirCommand
        {
            get
            {
                _workingDirCommand = _workingDirCommand ?? new RelayCommand(param => WorkingDirClick());
                return _workingDirCommand;
            }
        }

        private void WorkingDirClick()
        {
            _folderBrowser.SelectedPath = PlugInSettings.Default.PathToUnsignedInvoices;
            _folderBrowser.ShowNewFolderButton = true;
            _folderBrowser.Description = "Arbeitsverzeichnis wählen";
            var rc = _dlg.ShowFolderBrowserDialog(_folderBrowser);
            if (rc == DialogResult.OK)
            {
                WorkingDirectory = _folderBrowser.SelectedPath;
            }
        }

        #endregion
        #region Test Button

        private ICommand _testCommand;
        public ICommand TestCommand
        {
            get
            {
                _testCommand = _testCommand ?? new RelayCommand(TestClick);
                return _testCommand;
            }
        }

        private void TestClick(object o)
        {
            string fn = "";
            if (o is string)
            {
                fn = (string)o;
            }
            else
            {
                _openFile.InitialDirectory = PlugInSettings.Default.PathToUnsignedInvoices;
                _openFile.Filter = "Vorlagen (*.xmlt,*.xml)|*.xmlt;*.xml|All files (*.*)|*.*";
                DialogResult rc = _dlg.ShowOpenFileDialog(_openFile);
                if (rc != DialogResult.OK)
                {
                    return;
                }
                fn = _openFile.FileName;
            }
            if (!File.Exists(fn))
            {
                _dlg.ShowMessageBox("Datei " + _openFile.FileName + " wurde nicht gefunden.","Zustelldienst",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            XmlNamespaceManager Nspc = new XmlNamespaceManager(new NameTable());
            Nspc.AddNamespace("eb", "http://www.ebinterface.at/schema/4p1/");
            XDocument xDoc = XDocument.Load(fn);
            var bMail = GetMailAddress(xDoc, Nspc,BillerMail);
            var rMail = GetMailAddress(xDoc, Nspc, ReceipientMail);
            var argstr = string.Format(Arguments, AddQutes(fn), bMail, rMail);

        }

        private static string AddQutes(string str)
        {
            return '"' + str + '"';
        }
        private static string GetMailAddress(XDocument xDoc, XmlNamespaceManager Nspc, string xPath)
        {
            var bMail = xDoc.XPathSelectElement(xPath, Nspc).Value;
            if (string.IsNullOrEmpty(bMail)) bMail = "-nicht-vorhanden-";
            return bMail;
        }

        #endregion

    }
}
