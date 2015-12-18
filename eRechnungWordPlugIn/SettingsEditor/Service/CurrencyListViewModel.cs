namespace SettingsEditor.Service
{
    public class CurrencyListViewModel
    {
        private string _displayText;
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText == value)
                    return;
                _displayText = value;
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value)
                    return;
                _code = value;
            }
        }
    }
}
