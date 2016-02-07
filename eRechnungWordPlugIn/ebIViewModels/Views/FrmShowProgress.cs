using System;
using System.ComponentModel;
using ebIViewModels.ViewModels;
using WinFormsMvvm.DialogService;
using LogService;

namespace ebIViewModels.Views
{
    public partial class FrmShowProgress : FormService
    {
        private static BackgroundWorker _bw;
        public FrmShowProgress(ProgressViewModel view)
        {
            InitializeComponent();
            ViewModel = view;
            progressViewModelBindingSource.DataSource = view;
        }

        public override void SetBindingSource(object bindSrc)
        {
            ViewModel = bindSrc as ProgressViewModel;
            progressViewModelBindingSource.DataSource = ViewModel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _bw.CancelAsync();
            ExecuteRelayCommand(((ProgressViewModel)ViewModel).CancelCommand);
            this.Close();
        }

        private void StartBgWorker()
        {
            var progressView = (ProgressViewModel)ViewModel;
            progressView.Value = 0;            
            _bw = new BackgroundWorker();
            _bw.WorkerSupportsCancellation = false;
            _bw.DoWork += progressView.DoWork;
            _bw.WorkerReportsProgress = true;
            _bw.ProgressChanged += bw_ProgressChanged;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            _bw.RunWorkerAsync(progressView.PayLoad);
            
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgBar.PerformStep();
            ((ProgressViewModel) ViewModel).Value = pgBar.Value;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.TraceWrite(CallerInfo.Create(),"completed");
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void FrmShowProgress_Load(object sender, EventArgs e)
        {
            StartBgWorker();
        }
    }
}
