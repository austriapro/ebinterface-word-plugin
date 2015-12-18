using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using WinFormsFolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse
{
	/// <summary>
	/// Class wrapping System.Windows.Forms.FolderBrowserDialog, making it accept a ViewModel.
	/// </summary>
	public class FolderBrowserDialog : IDisposable
	{
		private  IFolderBrowserDialog folderBrowserDialog;
		private WinFormsFolderBrowserDialog concreteFolderBrowserDialog;
		

		/// <summary>
		/// Initializes a new instance of the <see cref="FolderBrowserDialog"/> class.
		/// </summary>
		/// <param name="folderBrowserDialog">The interface of a folder browser dialog.</param>
		public FolderBrowserDialog()
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
		public DialogResult ShowDialog(IFolderBrowserDialog folderBrowser)
		{
            this.folderBrowserDialog = folderBrowser;

            // Create concrete FolderBrowserDialog
            concreteFolderBrowserDialog = new WinFormsFolderBrowserDialog
            {
                Description = folderBrowserDialog.Description,
                SelectedPath = folderBrowserDialog.SelectedPath,
                ShowNewFolderButton = folderBrowserDialog.ShowNewFolderButton
            };

			DialogResult result = concreteFolderBrowserDialog.ShowDialog();

			// Update ViewModel
			folderBrowserDialog.SelectedPath = concreteFolderBrowserDialog.SelectedPath;

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


		~FolderBrowserDialog()
		{
			Dispose(false);
		}


		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (concreteFolderBrowserDialog != null)
				{
					concreteFolderBrowserDialog.Dispose();
					concreteFolderBrowserDialog = null;
				}
			}
		}

		#endregion
	}
}
