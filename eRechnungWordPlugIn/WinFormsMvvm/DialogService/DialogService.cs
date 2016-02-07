using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService.FrameworkDialogs;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialogMvvM = WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile.OpenFileDialog;
using SaveFileDialogMvvm = WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile.SaveFileDialog;
using FolderBrowserDialogMvvM = WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse.FolderBrowserDialog;
using LogService;

namespace WinFormsMvvm.DialogService
{
    /// <summary>
    /// Class responsible for abstracting ViewModels from Views.
    /// </summary>
    public class DialogService : IDialogService
    {
        private FolderBrowserDialogMvvM _folderBrowser;
        private SaveFileDialogMvvm _saveFileDialog;
        private OpenFileDialogMvvM _openFileDialog;
        private readonly IUnityContainer _uc;
        public DialogService(FrameworkDialogs.FolderBrowse.FolderBrowserDialog folderBrowser,
            FrameworkDialogs.SaveFile.SaveFileDialog saveFileDialog,
            FrameworkDialogs.OpenFile.OpenFileDialog openFileDialog,
            IUnityContainer container)
        {
            _folderBrowser = folderBrowser;
            _openFileDialog = openFileDialog;
            _saveFileDialog = saveFileDialog;
            _uc = container;
        }
        #region IDialogService Members

        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="form">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public DialogResult ShowDialog<T>(object viewModel) where T : FormService
        {
            // Form form2Show = form as Form;
            Log.TraceWrite(CallerInfo.Create(),"entering");
            var form2Show = _uc.Resolve<T>(new ParameterOverride("viewModel", viewModel));
            form2Show.SetBindingSource(viewModel);
            var rc = form2Show.ShowDialog();
            Log.TraceWrite(CallerInfo.Create(),"exiting rc={0}", rc.ToString());
            return rc;
        }

        //public DialogResult ShowDialog<T>(ViewModelBase viewModel) where T : FormService
        //{
        //    var form = _uc.Resolve<T>();
        //    return form.ShowDialog(viewModel);
        //}


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
        public DialogResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }

        public DialogResult ShowOpenFileDialog(IOpenFileDialog openFileDialog)
        {
            // OpenFileDialogMvvM openDlg = new OpenFileDialogMvvM(openFileDialog);
            return _openFileDialog.ShowDialog(openFileDialog);
        }

        public DialogResult ShowSaveFileDialog(ISaveFileDialog saveFileDialog)
        {
            //SaveFileDialogMvvm saveDlg = new SaveFileDialogMvvm(saveFileDialog);
            return _saveFileDialog.ShowDialog(saveFileDialog);
        }

        public DialogResult ShowFolderBrowserDialog(IFolderBrowserDialog folderBrowserDialog)
        {
            return _folderBrowser.ShowDialog(folderBrowserDialog);
        }

        #endregion

    }
}
