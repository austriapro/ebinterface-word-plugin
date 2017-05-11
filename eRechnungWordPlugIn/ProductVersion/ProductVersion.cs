using McSherry.SemanticVersioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductVersion
{
    public class ProductVersionInfo
    {
        public readonly string ChangeSetId;
        public readonly DateTime ChangeSetDate;
        public readonly string BuildName;
        public readonly DateTime CompileTime;
        public readonly string Title;
        public readonly string Version;
        public readonly SemanticVersion SemVersion;

        /*
        <ProductInfo>
  <Title>eRechnung - Massenversand ({0})</Title>
  <Version>2.3</Version>
  <CompileInfo>Release Enterprise</CompileInfo>
  <CompileTime>2016-01-22 09:12:23</CompileTime>
  <Branch>undef</Branch>
  <LastCommit>2016-01-25</LastCommit>
</ProductInfo>
        */

        private const string SChangeSetId = "ChangeSetId";
        private const string SChangeSetDate = "LastCommit";
        private const string SCompiledAs = "CompileInfo";
        private const string SVersion = "Version";
        private const string SCompileTime = "CompileTime";
        private const string STitle = "Title";

        public ProductVersionInfo(string productInfo)
        {
            XElement xdoc = XElement.Parse(productInfo);
            var xCsId = GetElement(xdoc, SChangeSetId);
            ChangeSetId = xCsId.Value;

            var xCsDate = GetElement(xdoc, SChangeSetDate);
            ChangeSetDate = DateTime.Parse(xCsDate.Value);

            var xCsCT = GetElement(xdoc, SCompileTime);
            CompileTime = DateTime.Parse(xCsCT.Value);

            var xCsAs = GetElement(xdoc, SCompiledAs);
            BuildName = xCsAs.Value;
            var xCsTitle = GetElement(xdoc, STitle);
            Title = xCsTitle.Value;

            var xCSVersion = GetElement(xdoc, SVersion);
            Version = xCSVersion.Value;
            SemVersion = SemanticVersion.Parse(Version);
        }

        private XElement GetElement(XElement xDoc, string name)
        {
            if (xDoc.Elements(name).Any())
            {
                return xDoc.Elements(name).First();
            }
            return null;
        }
    }
  
}
