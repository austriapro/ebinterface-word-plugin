using System;
using System.Linq;
using System.Xml.Linq;

namespace ebIModels.Version
{
    public class TfsInfo
    {
        public readonly int ChangeSetId;
        public readonly DateTime ChangeSetDate;
        public readonly string CompiledAs;
        public readonly DateTime CompileTime;

        private const string SChangeSetId = "ChangeSetID";
        private const string SChangeSetDate = "ChangeSetDate";
        private const string SCompiledAs = "CompileInfo";
        private const string SCompileTime = "CompileTime";

        public TfsInfo(XElement xdoc)
        {
            var xCsId = GetElement(xdoc, SChangeSetId);
            ChangeSetId = int.Parse(xCsId.Value);

            var xCsDate = GetElement(xdoc, SChangeSetDate);
            ChangeSetDate = DateTime.Parse(xCsDate.Value);

            var xCsCT = GetElement(xdoc, SCompileTime);
            CompileTime = DateTime.Parse(xCsCT.Value);

            var xCsAs = GetElement(xdoc, SCompiledAs);
            CompiledAs = xCsAs.Value;

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
