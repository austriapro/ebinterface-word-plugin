using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;


namespace WinFormsMvvm.Controls
{
    public class RibbonCommandButton : RelayCommand
    {
        public RibbonCommandButton(Action<object> execute) : base(execute)
        {
        }

        public RibbonCommandButton(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {
        }

        public RibbonCommandButton(Action<object> execute, Predicate<object> canExecute, Predicate<object> isVisible)
            : base(execute, canExecute)
        {
            _isVisible = isVisible;
        }

        public object Tag { get; set; }

        readonly Predicate<object> _isVisible;

        [DebuggerStepThrough]
        public bool IsVisible(object parameter)
        {
            return _isVisible == null || _isVisible(parameter);
        }

        public event EventHandler VisibilityChanged;

        public void RaiseVisibilityChanged()
        {
            if (VisibilityChanged != null)
            {
                VisibilityChanged(this, EventArgs.Empty);
            }
        }

    }
}
