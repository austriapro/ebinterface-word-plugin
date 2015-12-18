using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SettingsManager;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace SettingsEditor.ViewModels
{
    public class UidAbfrageSettingsViewModel : ViewModelBase
    {
        public UidAbfrageSettingsViewModel(IDialogService dlg) : base(dlg)
        {
            _teilnehmer = PlugInSettings.Default.FinOnlTeilnehmer;
            _benutzer = PlugInSettings.Default.FinOnlBenutzer;
        }
        #region Properties
        private string _teilnehmer;
        public string Teilnehmer
        {
            get { return _teilnehmer; }
            set
            {
                if (_teilnehmer == value)
                    return;
                _teilnehmer = value;
                OnPropertyChanged();
            }
        }
        private string _benutzer;
        public string Benutzer
        {
            get { return _benutzer; }
            set
            {
                if (_benutzer == value)
                    return;
                _benutzer = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Speichern Command
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => saveClick());
                return _saveCommand;
            }
        }
        private void saveClick()
        {
            PlugInSettings.Default.FinOnlTeilnehmer = Teilnehmer;
            PlugInSettings.Default.FinOnlBenutzer = Benutzer;
            PlugInSettings.Default.Save();
        }
        #endregion
    }
}
