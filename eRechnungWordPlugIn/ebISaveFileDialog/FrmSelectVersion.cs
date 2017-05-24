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
using static ebIModels.Schema.InvoiceType;

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

        public ebIVersion SelectedVersion { get; set; }
      
        public void SetSelectedVersion(ebIVersion version)
        {
            comboBox1.SelectedItem = version.ToString();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sel = comboBox1.SelectedItem as string;
            SelectedVersion = (ebIVersion)Enum.Parse(typeof(ebIVersion), sel);

        }
    }

}
