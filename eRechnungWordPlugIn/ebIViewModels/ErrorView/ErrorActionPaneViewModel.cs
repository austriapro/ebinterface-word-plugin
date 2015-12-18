using System;
using System.ComponentModel;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;

namespace ebIViewModels.ErrorView
{
    public class ErrorActionPaneViewModel : ViewModelBase
    {
        public const string PublishToPanelEvent = "PublishToPanelEvent";
        public const string ClearPanelEvent = "ClearPanelEvent";        

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


        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                _clearCommand = _clearCommand ?? new RelayCommand(param => ClearClick());
                return _clearCommand;
            }
        }

        private void ClearClick()
        {
            Message = "";
            ErrorList.Clear();
        }
        
        [SubscribesTo(PublishToPanelEvent)]
        public void OnErrorPublish(object sender, EventArgs ea)
        {
            ErrorPublishSingleEventArgs e = ea as ErrorPublishSingleEventArgs;
            if (e.Message!=null)
            {
                Message = e.Message;
            }
            ErrorList.Add(new ErrorViewModel()
            {
                Description = e.Description,
                Severity = (e.Severity+" ").Substring(0,1),
                FieldName = e.FieldName
            });
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
