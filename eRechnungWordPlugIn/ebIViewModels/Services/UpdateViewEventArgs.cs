using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIViewModels.Services
{
    public class UpdatePropertyEventArgs : EventArgs
    {
        public const string UpdateDropDownSelection = "UpdateDropDownSelection";
        public const string UpdateProtectedProperty = "UpdatedProtectedProperty";
        public const string UpdateDocTable = "UpdateDocTable";
        public const string ShowPanelEvent = "ShowMessagePanel";

        public string PropertyName { get; set; }
        public object Value { get; set; }
    }    
}
