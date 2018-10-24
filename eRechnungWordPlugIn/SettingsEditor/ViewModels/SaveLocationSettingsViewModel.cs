using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using SettingsManager;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;

namespace SettingsEditor.ViewModels
{
    public class SaveLocationSettingsViewModel :ViewModelBase
    {
        private readonly IUnityContainer _uc;
        private readonly IFolderBrowserDialog _folderBrowser;
        public SaveLocationSettingsViewModel(IUnityContainer uc, IDialogService dialogService, IFolderBrowserDialog folderBrowser)
        {
            _uc = uc;
            _dlg = dialogService;
            _folderBrowser = folderBrowser;
            _templatePath = PlugInSettings.Default.PathToInvoiceTemplates;
            _signedPath = PlugInSettings.Default.PathToSignedInvoices;
            _unsignedPath = PlugInSettings.Default.PathToUnsignedInvoices;
           
        }
        private string _templatePath;
        public string TemplatePath
        {
            get { return _templatePath; }
            set
            {
                if (_templatePath == value)
                    return;
                _templatePath = value;
                OnPropertyChanged();
            }
        }

        private string _unsignedPath;
        public string UnsignedPath
        {
            get { return _unsignedPath; }
            set
            {
                if (_unsignedPath == value)
                    return;
                _unsignedPath = value;
                OnPropertyChanged();
            }
        }

        private string _signedPath;
        public string SignedPath
        {
            get { return _signedPath; }
            set
            {
                if (_signedPath == value)
                    return;
                _signedPath = value;
                OnPropertyChanged();
            }
        }

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
            if (!ValidateDirectory(TemplatePath)) return;
            if (!ValidateDirectory(UnsignedPath)) return;
            PlugInSettings.Default.PathToInvoiceTemplates = _templatePath;
            PlugInSettings.Default.PathToSignedInvoices = _signedPath;
            PlugInSettings.Default.PathToUnsignedInvoices = _unsignedPath;
            PlugInSettings.Default.Save();
        }

        private bool ValidateDirectory(string path2Check)
        {
            if (!Directory.Exists(path2Check))
            {
                var rc =
                    _dlg.ShowMessageBox(
                        "Verzeichnis " + path2Check + " wurde nicht gefunden. Soll es angelegt werden?",
                        "Einstellungen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rc != DialogResult.Yes)
                {
                    return false;
                }
                Directory.CreateDirectory(path2Check);
            }
            return true;
        }

        #endregion

        #region Path to Unsigned
        private ICommand _unsignedPathCommand;
        public ICommand UnsignedPathCommand
        {
            get
            {
                _unsignedPathCommand = _unsignedPathCommand ?? new RelayCommand(UnsignedPathClick);
                return _unsignedPathCommand;
            }
        }
        private void UnsignedPathClick(object o)
        {
            if (o != null)
            {
                _unsignedPath = (string) o;
            }

            _folderBrowser.SelectedPath = _unsignedPath;
            _folderBrowser.Description = "Pfad für unsignierte eRechnungen";
            _folderBrowser.ShowNewFolderButton = true;
            var rc = _dlg.ShowFolderBrowserDialog(_folderBrowser);
            if (rc == DialogResult.OK)
            {
                UnsignedPath = _folderBrowser.SelectedPath;
            }
        }
        #endregion

        #region Path to Templates

        private ICommand _templatePathCommand;
        public ICommand TemplatePathCommand
        {
            get
            {
                _templatePathCommand = _templatePathCommand ?? new RelayCommand(TemplatePathClick);
                return _templatePathCommand;
            }
        }

        private void TemplatePathClick(object o)
        {
            if (o != null)
            {
                _templatePath = (string)o;
            }

            _folderBrowser.SelectedPath = _unsignedPath;
            _folderBrowser.Description = "Pfad für Vorlagen";
            _folderBrowser.ShowNewFolderButton = true;
            var rc = _dlg.ShowFolderBrowserDialog(_folderBrowser);
            if (rc == DialogResult.OK)
            {
                TemplatePath = _folderBrowser.SelectedPath;
            }
        }
        #endregion



    }
}
