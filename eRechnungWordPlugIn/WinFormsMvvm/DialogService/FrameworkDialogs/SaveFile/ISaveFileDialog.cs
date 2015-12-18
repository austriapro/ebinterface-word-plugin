using System.Security.Cryptography.X509Certificates;

namespace WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile
{
	/// <summary>
	/// Interface describing the OpenFileDialog.
	/// </summary>
	public interface ISaveFileDialog : IFileDialog
	{
        bool CreatePrompt { get; set; }
        bool OverwritePrompt { get; set; }
	}
}
