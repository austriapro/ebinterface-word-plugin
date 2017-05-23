using FileDialogExtenders;
using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsSaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile
{
    /// <summary>
    /// Class wrapping System.Windows.Forms.OpenFileDialog, making it accept a IOpenFileDialog.
    /// </summary>
    public class SaveFileDialog : IDisposable
    {
        private ISaveFileDialog saveFileDialog;
        private WinFormsSaveFileDialog concreteOpenFileDialog;


        /// <summary>
        /// Initializes a new instance of the <see cref="System.Windows.Forms.OpenFileDialog"/> class.
        /// </summary>
        /// <param name="saveFileDialog">The interface of a open file dialog.</param>
        public SaveFileDialog()
        {


        }


        /// <summary>
        /// Runs a common dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">
        /// Any object that implements System.Windows.Forms.IWin32Window that represents the top-level
        /// window that will own the modal dialog box.
        /// </param>
        /// <returns>
        /// System.Windows.Forms.DialogResult.OK if the user clicks OK in the dialog box; otherwise,
        /// System.Windows.Forms.DialogResult.Cancel.
        /// </returns>
        public DialogResult ShowDialog(ISaveFileDialog saveFile)
        {
            this.saveFileDialog = saveFile;

            var saveFileDlg = new WinFormsSaveFileDialog();

            // Create concrete OpenFileDialog
            concreteOpenFileDialog = new WinFormsSaveFileDialog
            {
                AddExtension = saveFileDialog.AddExtension,
                CheckFileExists = saveFileDialog.CheckFileExists,
                CheckPathExists = saveFileDialog.CheckPathExists,
                DefaultExt = saveFileDialog.DefaultExt,
                FileName = saveFileDialog.FileName,
                Filter = saveFileDialog.Filter,
                InitialDirectory = saveFileDialog.InitialDirectory,
                Title = saveFileDialog.Title,
                CreatePrompt = saveFileDialog.CreatePrompt,
                OverwritePrompt = saveFileDialog.OverwritePrompt

            };

            DialogResult result = concreteOpenFileDialog.ShowDialog();

            // Update ViewModel
            saveFileDialog.FileName = concreteOpenFileDialog.FileName;
            saveFileDialog.FileNames = concreteOpenFileDialog.FileNames;

            return result;
        }

        public DialogResult ShowDialog<T>(ISaveFileDialog saveFile, T userForm) where T : FileDialogControlBase
        {

            userForm.FileDlgAddExtension = saveFile.AddExtension;
            userForm.FileDlgCheckFileExists = saveFile.CheckFileExists;
            //userForm.MSDialog.CheckPathExists = saveFile.CheckPathExists;
            userForm.FileDlgDefaultExt = saveFile.DefaultExt;
            userForm.FileDlgFileName = saveFile.FileName;
            userForm.FileDlgFilter = saveFile.Filter;
            userForm.FileDlgInitialDirectory = saveFile.InitialDirectory;
            //userForm.MSDialog.Title = saveFile.Title;
            userForm.FileDlgType = Win32Types.FileDialogType.SaveFileDlg;
            //userForm.MSDialog.CreatePrompt = saveFile.CreatePrompt;
            //userForm.MSDialog.OverwritePrompt = saveFile.OverwritePrompt;
            DialogResult result = userForm.ShowDialog();
            saveFile.FileName = userForm.FileDlgFileName;
            saveFile.FileNames = userForm.FileDlgFileNames;
            return result;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~SaveFileDialog()
        {
            Dispose(false);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (concreteOpenFileDialog != null)
                {
                    concreteOpenFileDialog.Dispose();
                    concreteOpenFileDialog = null;
                }
            }
        }

        #endregion
    }
}
