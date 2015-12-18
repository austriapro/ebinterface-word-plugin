using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;

namespace WinFormsMvvm.DialogService
{
    /// <summary>
    /// Interface responsible for abstracting ViewModels from Views.
    /// </summary>

    public interface IDialogService
    {

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>

        // DialogResult ShowDialog();

        // DialogResult RunProgressBar(ViewModelBase viewModel);
        DialogResult ShowDialog<T>(object viewModel) where T : FormService;

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the message box.
        /// </param>
        /// <param name="messageBoxText">A string that specifies the text to display.</param>
        /// <param name="caption">A string that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A MessageBoxButton value that specifies which button or buttons to display.
        /// </param>
        /// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>
        /// A MessageBoxResult value that specifies which message box button is clicked by the user.
        /// </returns>
        DialogResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon);


        /// <summary>
        /// Shows the OpenFileDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="openFileDialog">The interface of a open file dialog.</param>
        /// <returns>DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
        DialogResult ShowOpenFileDialog(IOpenFileDialog openFileDialog);

        DialogResult ShowSaveFileDialog(ISaveFileDialog saveFileDialog);

        /// <summary>
        /// Shows the FolderBrowserDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="folderBrowserDialog">The interface of a folder browser dialog.</param>
        /// <returns>The DialogResult.OK if successful; otherwise DialogResult.Cancel.</returns>
        DialogResult ShowFolderBrowserDialog(IFolderBrowserDialog folderBrowserDialog);
    }
}
