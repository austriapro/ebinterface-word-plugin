using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace ebIModels.Services
{
    public class ResourceService : IResourceService
    {
        private const string ResourcePath = "ebIModels.";

        public string ReadXmlString(string resouceFileName)
        {
            return ReadXmlString(resouceFileName, null);
        }

        public string ReadXmlString(string resouceFileName, string path)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resouceFile;
            if (string.IsNullOrEmpty(path))
            {
                resouceFile = ResourcePath + "XmlData." + resouceFileName;

            }
            else
            {
                resouceFile = ResourcePath + path + "." + resouceFileName;

            }
            Stream stream = asm.GetManifestResourceStream(resouceFile);
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
        public XElement ReadXmlDocument(string resouceFileName)
        {
            return ReadXmlDocument(resouceFileName, null);
        }

        public XElement ReadXmlDocument(string resouceFileName, string path)
        {
            string xmlStr = ReadXmlString(resouceFileName, path);
            XElement xdoc = XElement.Parse(xmlStr);
            return xdoc;
        }

 
        public byte[] ReadBinaryData(string fileName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resourceFile;
            resourceFile = ResourcePath + "XmlData." + fileName;
            Stream stream = asm.GetManifestResourceStream(resourceFile);
            BinaryReader reader = new BinaryReader(stream);
            var len = (int)stream.Length;
            var bytes = new byte[len];
            reader.Read(bytes, 0, len);
            reader.Close();
            stream.Close();
            return bytes;
        }
    }
}