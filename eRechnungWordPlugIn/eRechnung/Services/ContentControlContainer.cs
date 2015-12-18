using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIViewModels;
using ebIViewModels.Services;
using Word = Microsoft.Office.Interop.Word;
using Tools = Microsoft.Office.Tools;

namespace eRechnung.Services
{
    public class ContentControlContainer
    {
        public string VmProperty { get; set; }
        public Type TargetType { get; set; }
        public Tools.Word.ContentControlBase CcControl { get; set; }
        public DropDownListViewModels DownListView { get; set; }    
        
    }
}
