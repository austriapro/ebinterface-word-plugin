using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.Controls;
using ebIModels.Services;
using System.Windows.Input;

namespace ebIViewModels.RibbonViewModels
{
    public class DownloadViewModel : ViewModelBase
    {
        private IUnityContainer _uc;
        private IDialogService _dlgService;
        private ProductInfo prod;
        const string _allReleases = "https://github.com/austriapro/ebinterface-word-plugin/releases";
        public DownloadViewModel(IUnityContainer uc, IDialogService dlgService)
        {
            _uc = uc;
            _dlgService = dlgService;
            prod = new ProductInfo(); // .VersionInfo;

            _NewVersion = $"Die Version {prod.LatestVersion} ist jetzt zum Download bereit.";
            _AllVersionsUrl = _allReleases;
            _LatestVersionUrl = prod.LatestReleaseHtmlUrl;
        }

        #region LatestVersionUrl - string
        private string _LatestVersionUrl;
        public string LatestVersionUrl
        {
            get { return _LatestVersionUrl; }
            set
            {
                if (_LatestVersionUrl == value)
                    return;
                _LatestVersionUrl = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region AllVersionsUrl - string
        private string _AllVersionsUrl;
        public string AllVersionsUrl
        {
            get { return _AllVersionsUrl; }
            set
            {
                if (_AllVersionsUrl == value)
                    return;
                _AllVersionsUrl = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NewVersion - string
        private string _NewVersion;
        public string NewVersion
        {
            get { return _NewVersion; }
            set
            {
                if (_NewVersion == value)
                    return;
                _NewVersion = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private ICommand _DownloadCommand;
        public ICommand DownloadCommand
        {
            get
            {
                _DownloadCommand = _DownloadCommand ?? new RelayCommand(param => DownloadClick());
                return _DownloadCommand;
            }
        }

        private void DownloadClick()
        {
            System.Diagnostics.Process.Start(prod.LatestReleaseHtmlUrl);
        }


        private ICommand _ShowAllCommand;
        public ICommand ShowAllCommand
        {
            get
            {
                _ShowAllCommand = _ShowAllCommand ?? new RelayCommand(param => ShowAllClick());
                return _ShowAllCommand;
            }
        }

        private void ShowAllClick()
        {
            System.Diagnostics.Process.Start(_allReleases);
        }
    }
}
