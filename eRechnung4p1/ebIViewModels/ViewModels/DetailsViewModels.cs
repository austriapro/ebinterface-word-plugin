using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Shapes;
using ebIModels.Models;
using ebIViewModels.Views;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using System.Windows.Input;

namespace ebIViewModels.ViewModels
{
    public class DetailsViewModels : ViewModelBase
    {

        private BindingList<DetailsViewModel> _detailsViewList = new BindingList<DetailsViewModel>();
        /// <summary>
        /// Liste der Rechnungszeilen (listlineitem)
        /// </summary>
        public BindingList<DetailsViewModel> DetailsViewList
        {
            get { return _detailsViewList; }
            set
            {
                if (_detailsViewList == value)
                    return;
                _detailsViewList = value;
                OnPropertyChanged();
                IsEditable = _detailsViewList.Any();
            }
        }

        private bool _isEditable;
        /// <summary>
        /// Comment
        /// </summary>
        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                if (_isEditable == value)
                    return;
                _isEditable = value;
                OnPropertyChanged();
            }
        }

        private IUnityContainer _uc;
        private bool _bestPosRequired;
        public InvoiceSubtypes.ValidationRuleSet CurrentRuleSet{get;set;}

        public DetailsViewModels(IUnityContainer uc, IDialogService dialogService, bool bestPosRequired, 
            InvoiceSubtypes.ValidationRuleSet currentRuleSet)
        {
            _uc = uc;
            _dlg = dialogService;
            _bestPosRequired = bestPosRequired;
            CurrentRuleSet = currentRuleSet;
            // _DetailsViewList = new ObservableCollection<DetailsViewModel>();
        }


        #region Commands

        private ICommand _clearCommand;

        public ICommand ClearCommand
        {
            get
            {
                _clearCommand = _clearCommand ?? new RelayCommand(param => ClearClick(), CanSaveExecute);
                return _clearCommand;
            }
        }


        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => saveClick(), CanSaveExecute);
                return _saveCommand;
            }
        }

        private bool CanSaveExecute(object obj)
        {
            return DetailsViewList.Any();
        }


        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get
            {
                _addCommand = _addCommand ?? new RelayCommand(param => AddClick(),CanAddDetails);
                return _addCommand;
            }
        }

        private bool CanAddDetails(object obj)
        {
            if (CurrentRuleSet != InvoiceSubtypes.ValidationRuleSet.Government) return true;
            if (DetailsViewList.Count < 999) return true;
            return false;
        }

        private ICommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new RelayCommand(DeleteClick, CanSaveExecute);
                return _deleteCommand;
            }
        }


        private ICommand _editCommand;

        public ICommand EditCommand
        {
            get
            {
                _editCommand = _editCommand ?? new RelayCommand(EditClick, CanSaveExecute);
                return _editCommand;
            }
        }

        #endregion

        #region Command Methods

        private void EditClick(object parm)
        {
            var detail = _uc.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", _bestPosRequired));
            detail = _detailsViewList[(int) parm];
            var rc = _dlg.ShowDialog<FrmDetailsEdit>(detail);
            if (rc == DialogResult.OK)
            {
                _detailsViewList[(int) parm] = detail;                
                IsEditable = _detailsViewList.Any();
                OnPropertyChanged("DetailsViewList");
            }
        }


        private void DeleteClick(object parm)
        {
            var rc = _dlg.ShowMessageBox("Wollen Sie diese Detailposition wirklich löschen?",
                "Detailposition löschen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (rc == DialogResult.Yes)
            {
                int i = (int) parm;
                DetailsViewList.RemoveAt(i);
                IsEditable = DetailsViewList.Any();
                OnPropertyChanged("DetailsViewList");
            }
        }

        private void AddClick()
        {
            DetailsViewModel view = _uc.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", _bestPosRequired));
            var rc = _dlg.ShowDialog<FrmDetailsEdit>(view);
            if (rc == DialogResult.OK)
            {
                DetailsViewList.Add(view);
                IsEditable = DetailsViewList.Any();
                OnPropertyChanged("DetailsViewList");
            }
        }

        private void saveClick()
        {
            throw new NotImplementedException();
        }

        private void ClearClick()
        {
            for (int i = (DetailsViewList.Count - 1); i >= 0; i--)
            {
                DetailsViewList.RemoveAt(i);
            }
            IsEditable = DetailsViewList.Any();
            OnPropertyChanged("DetailsViewList");
        }

        #endregion



    }
}
