using System.Reflection;
using ebIModels.Services;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using SettingsManager;
using System;
using System.ComponentModel;

namespace ebIViewModels.RibbonViewModels
{
    public class AboutViewModel : ViewModelBase
    {


        public AboutViewModel(IDialogService dialog):base(dialog)
        {
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
            _HtmlUrl = new Uri(prod.LatestReleaseHtmlUrl);
            
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
        #region HtmlUrl - string
        private Uri _HtmlUrl = new Uri("http://sample.url.com");
        public Uri HtmlUrl
        {
            get { return _HtmlUrl; }
            set
            {
                if (_HtmlUrl == value)
                    return;
                _HtmlUrl = value;
                OnPropertyChanged();
            }
        }
        #endregion

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