using System.Xml.Linq;

namespace ebIModels.Services
{
    public interface IResourceService
    {
        string ReadXmlString(string resouceFileName);
        string ReadXmlString(string resouceFileName, string path);
        XElement ReadXmlDocument(string resouceFileName);
        XElement ReadXmlDocument(string resouceFileName, string path);
        byte[] ReadBinaryData(string fileName);
    }
}
