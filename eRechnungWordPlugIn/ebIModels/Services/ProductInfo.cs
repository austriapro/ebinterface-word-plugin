using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ProductVersion;

namespace ebIModels.Services
{
    public class ProductInfo
    {

        public ProductVersionInfo VersionInfo
        {
            get; private set;
        }

        public ProductInfo()
        {
            VersionInfo = new ProductVersionInfo(Properties.Resources.ProductInfo);
        }

        public static string GetProductInfoXml()
        {
            return Properties.Resources.ProductInfo;
        }
    }
}