using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ProductVersion;
using Octokit;
using System.Linq;
using McSherry.SemanticVersioning;
using LogService;
using System;

namespace ebIModels.Services
{
    /// <summary>
    /// Provides information about the installed software version
    /// </summary>
    public class ProductInfo
    {
        private const string gitUser = "austriapro";
        private const string gitProject = "ebinterface-word-plugin";
        private Release latestRelease;
        private bool _isNewReleaseAvailable = false;
        public bool IsNewReleaseAvailable
        {
            get
            {
                return _isNewReleaseAvailable;
            }
        }
        public string LatestVersion { get { return latestRelease?.TagName; } }
        public string LatestReleaseHtmlUrl { get { return latestRelease?.HtmlUrl; } }
        public ProductVersionInfo VersionInfo
        {
            get; private set;
        }

        public ProductInfo()
        {
            VersionInfo = new ProductVersionInfo(Properties.Resources.ProductInfo);
#if DEBUG
            latestRelease = null;
#else
            GetLatestReleaseFromGitHub();
#endif
        }

        private void GetLatestReleaseFromGitHub()
        {
            var client = new GitHubClient(new ProductHeaderValue(gitUser));
            var releases = client.Repository.Release.GetAll(gitUser, gitProject);
            try
            {
                releases.Wait();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (System.Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                latestRelease = new Release();
                _isNewReleaseAvailable = false;
                DateTime restDateTime = DateTime.Now;

                Log.LogWrite(CallerInfo.Create(), Log.LogPriority.High, $"Abfrage für neue Release fehlgeschlagen: {releases.Exception.InnerException.Message}");

                Log.LogWrite(CallerInfo.Create(), Log.LogPriority.High, $"Github Reset: {restDateTime:G} ");
                _isNewReleaseAvailable = false;
                return;
            }
            var releaseItems = releases.Result.Where(x => x.Prerelease == false).OrderByDescending(p => p.CreatedAt);
            if (releaseItems.Any())
            {
                latestRelease = releaseItems.OrderByDescending(p => p.CreatedAt).First();
                Log.LogWrite(CallerInfo.Create(), Log.LogPriority.High, $"Release at GitHub: {latestRelease.TagName}");
                // Release Tag auf github muss die Syntax Vn.n.n+n haben sonst geht das hier schief
                var latestVersion = SemanticVersion.Parse(latestRelease.TagName, ParseMode.AllowPrefix);
                _isNewReleaseAvailable = false;
#if DEBUGx
                _isNewReleaseAvailable = true;
#endif
                if (latestVersion > VersionInfo.SemVersion)
                {
                    _isNewReleaseAvailable = true;
                }
            }

        }
    }

}