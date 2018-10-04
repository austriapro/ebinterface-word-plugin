using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileDialogExtenders;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using ebIModels.Schema;
using ebISaveFileDialog;
using static ebIModels.Models.InvoiceModel;
using ebIModels.Models;

namespace ebISaveFileDialog 
{
    public partial class FrmSelectVersion : FileDialogControlBase
    {

        public FrmSelectVersion()
        {
            InitializeComponent();
            this.FileDlgType = Win32Types.FileDialogType.SaveFileDlg;
            this.FileDlgStartLocation = AddonWindowLocation.Bottom;
            List<string> ebiLIst = InvoiceFactory.GetVersionsWithSaveSupported().OrderByDescending(p => p).ToList();
            comboBox1.DataSource = ebiLIst;
            // comboBox1.SelectedItem = SelectedVersion.ToString();
            //comboBox1.SelectedText = SelectedVersion.ToString();
            //  this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSourceReStellerSetting, "SelectedVersion", true));
        }

        public EbIVersion SelectedVersion { get; set; }
      
        public void SetSelectedVersion(EbIVersion version)
        {
            comboBox1.SelectedItem = version.ToString();
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sel = comboBox1.SelectedItem as string;
            SelectedVersion = (EbIVersion)Enum.Parse(typeof(EbIVersion), sel);

        }
    }

}
