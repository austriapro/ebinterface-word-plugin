using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.ErrorView;
using ebIViewModels.Services;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using LogService;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        public ProgressViewModel(IDialogService dlg)
            : base(dlg)
        {

        }
        private string _description;
        /// <summary>
        /// Comment
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }

        private int _step;
        /// <summary>
        /// Comment
        /// </summary>
        public int Step
        {
            get { return _step; }
            set
            {
                if (_step == value)
                    return;
                _step = value;
                OnPropertyChanged();
            }
        }

        private int _minimum;
        /// <summary>
        /// Comment
        /// </summary>
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                if (_minimum == value)
                    return;
                _minimum = value;
                OnPropertyChanged();
            }
        }

        private int _maximum;
        /// <summary>
        /// Comment
        /// </summary>
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (_maximum == value)
                    return;
                _maximum = value;
                OnPropertyChanged();
            }
        }
        
        private ProgressBarStyle _style = ProgressBarStyle.Continuous;
        /// <summary>
        /// Comment
        /// </summary>
        public ProgressBarStyle Style
        {
            get { return _style; }
            set
            {
                if (_style == value)
                    return;
                _style = value;
                OnPropertyChanged();
            }
        }

        private int _value;
        /// <summary>
        /// Comment
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                CountTracking = string.Format("Schritt {0} von {1}", Value, Maximum);
                OnPropertyChanged();
            }
        }


        private string _countTracking;
        /// <summary>
        /// Comment
        /// </summary>
        public string CountTracking
        {
            get { return _countTracking; }
            set
            {
                if (_countTracking == value)
                    return;
                _countTracking = value;
                Log.TraceWrite(value);
                OnPropertyChanged();
            }
        }

        private bool _isCancelled;
        /// <summary>
        /// Comment
        /// </summary>
        public bool IsCancelled
        {
            get { return _isCancelled; }
            set
            {
                if (_isCancelled == value)
                    return;
                _isCancelled = value;
                OnPropertyChanged();
            }
        }

        private object _payLoad;
        /// <summary>
        /// Comment
        /// </summary>
        public object PayLoad
        {
            get { return _payLoad; }
            set
            {
                if (_payLoad == value)
                    return;
                _payLoad = value;
                OnPropertyChanged();
            }
        }

        public Exception ex;
        public DoWorkEventHandler DoWork;

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                _cancelCommand = _cancelCommand ?? new RelayCommand(param => CancelClick());
                return _cancelCommand;
            }
        }

        private void CancelClick()
        {
            IsCancelled = true;
        }

        //[SubscribesTo(ProgressViewModel.CancelClicked)]
        //public void ProgressCancelClicked(object sender, EventArgs e)
        //{
        //    IsCancelled = true;
        //}
    }
}
