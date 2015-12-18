using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Office.Tools.Ribbon;
using WinFormsMvvm.Controls;

namespace eRechnung.Services
{
    public class RibbonCommandBinding
    {
        /// <summary>
        /// Id des Ribbon Controls
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Command aus dem ViewModel
        /// </summary>
        public RibbonCommandButton Command { get; set; }

        public RibbonButton Button { get; set; }

        public bool PropagateInvoice { get; set; }
    
    }
}
