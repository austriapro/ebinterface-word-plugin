using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ebIModels.Services;
using IResourceService = ebIModels.Services.IResourceService;

namespace ebIModels.Models
{
    public class CountryCodesModels : ICountryCodes
    {
        private List<CountryCodeModel> _countryCodes;
        private const string CountryCodeXml = "iso_3166-1_laender.xml";
        public List<CountryCodeModel> GetCountryCodeList()
        {
            if (_countryCodes != null)
            {
                return _countryCodes;
            }
            IResourceService xmlRes = new ResourceService();
            var xDoc = xmlRes.ReadXmlDocument(CountryCodeXml);
            IEnumerable<XElement> childs = from xel in xDoc.Elements() select xel;
            _countryCodes = (from xElement in childs
                             select new CountryCodeModel()
                             {
                                 Code = xElement.Element("Shortcut").Value,
                                 Country = getCountry(xElement.Element("Land").Value)
                             }).ToList<CountryCodeModel>();

            return _countryCodes;
        }

        /// <summary>
        /// Retourniert den 2. Teil des Ländernamens (siehe Wikipedia http://de.wikipedia.org/wiki/ISO-3166-1-Kodierliste)
        /// Dort haben manche Ländernamen ein !, mit dem der Name mit Umlauten angezeigt wird
        /// </summary>
        /// <param name="wert"></param>
        /// <returns></returns>
        private string getCountry(string wert)
        {
            if (wert.Contains("!"))
            {
                var splt = wert.Split('!');
                return splt.Length > 1 ? splt[1] : wert;
            }
            return wert;
        }
    }
}
