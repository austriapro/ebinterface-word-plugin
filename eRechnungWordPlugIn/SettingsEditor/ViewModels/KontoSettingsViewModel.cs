using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinFormsMvvm.Controls;

namespace SettingsEditor.ViewModels
{
    public class KontoSettingsViewModel
    {
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
            // throw new NotImplementedException();
        }

        #endregion
    }
}
