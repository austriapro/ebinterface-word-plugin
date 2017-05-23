using System.Reflection;
using ebIModels.Services;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using SettingsManager;
using System;
using System.ComponentModel;
using WinFormsMvvm.Controls;
using Microsoft.Practices.Unity;
using ebIViewModels.RibbonViews;

namespace ebIViewModels.RibbonViewModels
{
    public class AboutViewModel : ViewModelBase
    {

        private IUnityContainer _uc;
        public AboutViewModel(IUnityContainer uc, IDialogService dialog):base(dialog)
        {
            _uc = uc;
            string info = string.Empty;
            Assembly asm = Assembly.GetExecutingAssembly();
            //info = asm.FullName;
            string svnInfo = string.Empty;
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);
            if (attributes.Length > 0)
            {
                AssemblyDescriptionAttribute descriptionAttribute = attributes[0] as AssemblyDescriptionAttribute;
                svnInfo = descriptionAttribute.Description;

            }

            var prod = new ProductInfo(); // .VersionInfo;
            _productInfo = prod.VersionInfo.Title + ", Open Source Version";
            _releaseInfo = prod.VersionInfo.Version; // ProductInfo.GetTfsInfoString();
            _versionInfo = prod.VersionInfo.Version; // GetRunningVersion();

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                _versionInfo = string.Format($"{ad.CurrentVersion.Major}.{ad.CurrentVersion.Minor}.{ad.CurrentVersion.Build}.{ad.CurrentVersion.Revision}");
            }

            _NugetPackages = new BindingList<NugetPackage>(new NugetPackages().Packages);
            _IsNewReleaseAvailable = prod.IsNewReleaseAvailable;
            _NewVersion = $"Neue Version {prod.LatestVersion} verfügbar:";            
        }

        #region NugetPackages - NugetPackages
        private BindingList<NugetPackage> _NugetPackages;
        public BindingList<NugetPackage> NugetPackages
        {
            get { return _NugetPackages; }
            set
            {
                if (_NugetPackages == value)
                    return;
                _NugetPackages = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region IsNewReleaseAvailable - bool
        private bool _IsNewReleaseAvailable;
        public bool IsNewReleaseAvailable
        {
            get { return _IsNewReleaseAvailable; }
            set
            {
                if (_IsNewReleaseAvailable == value)
                    return;
                _IsNewReleaseAvailable = value;
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

        private RelayCommand _DownloadCommand;
        public RelayCommand DownloadCommand
        {
            get
            {
                _DownloadCommand = _DownloadCommand ?? new RelayCommand(param => DownloadClick());
                return _DownloadCommand;
            }
        }

        private void DownloadClick()
        {
            var dwnVm = _uc.Resolve<DownloadViewModel>();
            _dlg.ShowDialog<FrmUpdateDownload>(dwnVm);
        }

        private string _versionInfo;
        /// <summary>
        /// Comment
        /// </summary>
        public string VersionInfo
        {
            get { return _versionInfo; }
            set
            {
                if (_versionInfo == value)
                    return;
                _versionInfo = value;
                OnPropertyChanged();
            }
        }

        private string _productInfo;
        /// <summary>
        /// Comment
        /// </summary>
        public string ProductInfoText   
        {
            get { return _productInfo; }
            set
            {
                if (_productInfo == value)
                    return;
                _productInfo = value;
                OnPropertyChanged();
            }
        }


        private string _releaseInfo;
        /// <summary>
        /// Comment
        /// </summary>
        public string ReleaseInfo
        {
            get { return _releaseInfo; }
            set
            {
                if (_releaseInfo == value)
                    return;
                _releaseInfo = value;
                OnPropertyChanged();
            }
        }


        private string GetRunningVersion()
        {
            try
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }



    }
}