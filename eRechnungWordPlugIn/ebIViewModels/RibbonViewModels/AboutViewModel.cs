using System.Reflection;
using ebIModels.Services;
using WinFormsMvvm;

namespace ebIViewModels.RibbonViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel()
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
            var prod = ProductInfo.GetProductInfo();
            _productInfo = prod.Title + " Vers. " + prod.Version;
            _releaseInfo = ProductInfo.GetTfsInfoString();
            _versionInfo = GetRunningVersion();
            //svnInfo = ebDoc.ThisDocument.GetAsmTitle();
            //this.lblProduct.Text = svnInfo;
            //svnInfo = ebDoc.ThisDocument.GetSVNInfo();
            //this.lblVersion.Text = svnInfo;
            //string TopLine = "AUSTRIAPRO - Word PlugIn für ebInterface " + InvoiceXML.ebInterface_Version.Replace("p", ".").Replace(@"/", "");
            //lblHeadline.Text = TopLine;
            //lblRelease.Text = GetRunningVersion();
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