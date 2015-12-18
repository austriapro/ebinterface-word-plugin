namespace WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile
{
	/// <summary>
	/// ViewModel of the OpenFileDialog.
	/// </summary>
	public class SaveFileDialogViewModel : FileDialogViewModel, ISaveFileDialog
	{
	    public bool CreatePrompt { get; set; }
	    public bool OverwritePrompt { get; set; }

	    public SaveFileDialogViewModel()
	    {
	        CreatePrompt = true;
	        OverwritePrompt = true;
	        CheckFileExists = false;
	        CheckPathExists = false;
	    }
	}
}
