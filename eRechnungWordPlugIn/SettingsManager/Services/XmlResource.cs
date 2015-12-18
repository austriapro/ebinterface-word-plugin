using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace SettingsManager.Services
{
    public static class ResourceService
    {
        private const string ResourcePath = "SettingsManager.XmlData.";

        public static string ReadXmlString(string resouceFileName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resouceFile = ResourcePath + resouceFileName;
            Stream stream = asm.GetManifestResourceStream(resouceFile);
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public static XElement ReadXmlDocument(string resouceFileName)
        {
            string xmlStr = ReadXmlString(resouceFileName);
            XElement xdoc = XElement.Parse(xmlStr);
            return xdoc;
        }




    }
}