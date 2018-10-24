using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Forms;

namespace WinFormsMvvm.Controls
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.
    /// See: http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        internal readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        
        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;           
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                _execute(parameter);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogService.Log.LogWrite(LogService.CallerInfo.Create(), LogService.Log.LogPriority.High, "{0} {1}",ex.Message, ex.StackTrace);
#if DEBUG
                throw;
#endif
            }
        }
        #endregion // ICommand Members
    }
}