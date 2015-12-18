using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ebIModels.Version;

namespace ebIModels.Services
{
    public class ProductInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }

        public static ProductInfo GetProductInfo()
        {
            ProductInfo prod = new ProductInfo();
            var xProd = GetXElement("ProductInfo.xml",null);
            prod.Title = xProd.Element("Title").Value;
            prod.Version = xProd.Element("Version").Value;
            return prod;
        }

        private static XElement GetXElement(string fileName, string path)
        {
            IResourceService xmlRes = new ResourceService();
            XElement xProd = xmlRes.ReadXmlDocument(fileName,path);
            return xProd;
        }
        /// <summary>
        /// Returns the AssemblyDescription Attribute
        /// </summary>
        /// <returns></returns>
        public static string GetTfsInfoString()
        {
            XElement xdoc = GetXElement("Version.xml","Version");
            var tfsInfo = new TfsInfo(xdoc);
            string svnInfo = "ChangeSet: " + tfsInfo.ChangeSetId.ToString() + " Compile: " +
                             tfsInfo.CompileTime.ToString("yyyy.MM.dd HH:mm:ss") + " (" + tfsInfo.CompiledAs + ")";
            return svnInfo;
        }

        public static TfsInfo GetTfsInfo()
        {
            XElement xdoc = GetXElement("Version.xml", "Version");
            var tfsInfo = new TfsInfo(xdoc);
            return tfsInfo;
        }

    }
}