using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvvm;

namespace ebIViewModels.ViewModels
{
    public class DetailsEditViewModel : ViewModelBase
    {

        private DetailsViewModel _detail;
        /// <summary>
        /// Comment
        /// </summary>
        public DetailsViewModel Detail
        {
            get { return _detail; }
            set
            {
                if (_detail == value)
                    return;
                _detail = value;
                OnPropertyChanged();
            }
        }


    }
}
