using WinFormsMvvm;

namespace ebIViewModels.ErrorView
{
    public class ErrorViewModel : ViewModelBase
    {
        private string _fieldName;

        /// <summary>
        /// Name des Feldes in dem der Fehler aufgetreten ist
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                if (_fieldName == value)
                    return;
                _fieldName = value;
                OnPropertyChanged();

            }
        }

        private string _severity;
        /// <summary>
        /// Comment
        /// </summary>
        public string Severity
        {
            get { return _severity; }
            set
            {
                if (_severity == value)
                    return;
                _severity = value;
                OnPropertyChanged();
            }
        }

        private string _description;

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
    }
}
