using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ebIModels.Services
{
    public class NugetPackage
    {
        public NugetPackage(string product, string authors, string licenseUrl)
        {
            Product = product;
            Authors = authors;
            LicenseUrl = licenseUrl;
        }
        public string Product { get; set; }

        public string Authors { get; set; }
        public string LicenseUrl { get; set; }
    }

    public class NugetPackages
    {
        public List<NugetPackage> Packages = new List<NugetPackage>()
        {
            new NugetPackage("EnterpriseLibrary.Common","Microsoft","http://www.opensource.org/licenses/ms-pl"),
            new NugetPackage("EnterpriseLibrary.Logging","Microsoft","http://www.opensource.org/licenses/ms-pl"),
            new NugetPackage("EnterpriseLibrary.Validation","Microsoft","http://www.opensource.org/licenses/ms-pl"),
            new NugetPackage("EnterpriseLibrary.Validation.Integration.WinForms","Microsoft","http://www.opensource.org/licenses/ms-pl"),
            new NugetPackage("McSherry.SemanticVersioning","Liam McSherry","https://github.com/McSherry/McSherry.SemanticVersioning/blob/v1.2.1/LICENCE.txt"),
            new NugetPackage("Octokit","GitHub","https://github.com/octokit/octokit.net/blob/master/LICENSE.txt"),
            new NugetPackage("RhinoMocks","ayende","http://hibernatingrhinos.com/open-source/rhino-mocks"),
            new NugetPackage("Unity","Microsoft","http://opensource.org/licenses/Apache-2.0"),
            new NugetPackage("Unity.Interception","Microsoft","http://opensource.org/licenses/Apache-2.0"),
            
        };
    }

}
