using System;
using System.ComponentModel;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using ebIModels.Services;
using ebIViewModels.RibbonViews;
using ebIViewModels.RibbonViewModels;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.ErrorView
{
    public class ErrorActionPaneViewModel : ViewModelBase
    {
        public const string PublishToPanelEvent = "PublishToPanelEvent";
        public const string ClearPanelEvent = "ClearPanelEvent";

        private IUnityContainer _uc;
        
        private BindingList<ErrorViewModel> _errors = new BindingList<ErrorViewModel>();
        public BindingList<ErrorViewModel> ErrorList
        {
            get { return _errors; }
            set
            {
                if (_errors == value)
                    return;
                _errors = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value)
                    return;
                _message = value;
                OnPropertyChanged();
            }
        }

        #region IsNewReleaseAvailable - bool
        private bool _IsNewReleaseAvailable;
        public bool IsNewReleaseAvailable
        {
            get { return _IsNewReleaseAvailable; }
            set
            {
                if (_IsNewReleaseAvailable == value)
                    return;
                _IsNewReleaseAvailable = value;
                OnPropertyChanged();
            }
        }
        #endregion
 

        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                _clearCommand = _clearCommand ?? new RelayCommand(param => ClearClick());
                return _clearCommand;
            }
        }


        #region Download
        private RelayCommand _DownloadCommand;
        public RelayCommand DownloadCommand
        {
            get
            {
                _DownloadCommand = _DownloadCommand ?? new RelayCommand(param => DownloadClick());
                return _DownloadCommand;
            }
        }

        private void DownloadClick()
        {
            var dwnVm = _uc.Resolve<DownloadViewModel>();
            _dlg.ShowDialog<FrmUpdateDownload>(dwnVm);
        }

        #endregion

        private void ClearClick()
        {
            Message = "";
            ErrorList.Clear();
        }
        public ErrorActionPaneViewModel(IUnityContainer uc, IDialogService dlg) : base(dlg)
        {
            var prod = new ProductInfo();
            _IsNewReleaseAvailable = prod.IsNewReleaseAvailable;
            _uc = uc;            

        }
        [SubscribesTo(PublishToPanelEvent)]
        public void OnErrorPublish(object sender, EventArgs ea)
        {
            ErrorPublishSingleEventArgs e = ea as ErrorPublishSingleEventArgs;
            try
            {
                if (e.Message != null)
                {
                    Message = e.Message;
                }
                ErrorList.Add(new ErrorViewModel()
                {
                    Description = e.Description,
                    Severity = (e.Severity + " ").Substring(0, 1),
                    FieldName = e.FieldName
                });
            }
            catch (Exception ex)
            {
                LogService.Log.LogWrite(LogService.CallerInfo.Create(), LogService.Log.LogPriority.High, $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                LogService.Log.LogWrite(LogService.CallerInfo.Create(), LogService.Log.LogPriority.High, $"{e.FieldName}: '{e.Description}'");
            }
        }

        [SubscribesTo(ClearPanelEvent)]
        public void OnErrorClear(object sender, EventArgs args)
        {
            ClearClick();
        }
    }



    public class ErrorPublishSingleEventArgs : EventArgs
    {
        public string Message;
        public string FieldName;
        public string Severity;
        public string Description;
    }

    
}
