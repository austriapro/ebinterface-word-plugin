using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ProductVersion;
using Octokit;
using System.Linq;

namespace ebIModels.Services
{
    /// <summary>
    /// Provides information about the installed software version
    /// </summary>
    public class ProductInfo
    {
        private string gitUser = "austriapro";
        private string gitProject = "ebinterface-word-plugin";
        private Release latestRelease;
        private bool _isNewReleaseAvailable = false;
        public bool IsNewReleaseAvailable
        {
            get
            {
                return _isNewReleaseAvailable;
            }
        }
        public string LatestReleaseHtmlUrl { get { return latestRelease?.HtmlUrl; }}
        public ProductVersionInfo VersionInfo
        {
            get; private set;
        }

        public ProductInfo()
        {
            VersionInfo = new ProductVersionInfo(Properties.Resources.ProductInfo);            
            getLatestReleaseFromGitHub();
        }

        private void getLatestReleaseFromGitHub()
        {
            var client = new GitHubClient(new ProductHeaderValue("austriapro"));
            var releases = client.Repository.Release.GetAll("austriapro", "ebinterface-word-plugin");
            releases.Wait();
            var releaseItems = releases.Result.OrderByDescending(p => p.CreatedAt);
            if (releaseItems.Any())
            {
                latestRelease = releaseItems.OrderByDescending(p=>p.CreatedAt).First();

            }

        }
    }
    
}