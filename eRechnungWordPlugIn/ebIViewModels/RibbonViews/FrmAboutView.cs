using System;
using System.Windows.Forms;
using ebIViewModels.RibbonViewModels;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.RibbonViews
{
  
    /// <summary>
    /// About Formular
    /// </summary>
    public partial class FrmAboutView : FormService
    {
        public FrmAboutView(AboutViewModel aboutView)
        {

            InitializeComponent();
            ViewModel = aboutView;
            aboutViewModelBindingSource.DataSource = ViewModel;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel2.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel1.Text);
        }

        //private string GetRunningVersion()
        //{
        //    try
        //    {
        //        return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
        //    }
        //    catch
        //    {
        //        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //    }
        //}

    }
}
