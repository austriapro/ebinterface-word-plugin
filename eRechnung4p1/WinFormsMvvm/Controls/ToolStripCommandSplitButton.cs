using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Input;

namespace WinFormsMvvm.Controls
{
    [System.ComponentModel.DesignerCategory("")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripCommandSplitButton : ToolStripSplitButton, IBindableComponent
    {
        private ICommand _command;
        private ControlBindingsCollection _dataBindings;
        private BindingContext _bindingContext;

        [DefaultValue(null)]
        [Browsable(false)]
        [Bindable(true)]
        public ICommand Command
        {
            get { return _command; }
            set
            {
                if (_command == value)
                    return;
                SetCommand(value);
            }
        }

        private void SetCommand(ICommand command)
        {
            if (_command != null)
            {
                _command.CanExecuteChanged -= CommandOnCanExecuteChanged;
            }

            _command = command;

            if (_command != null)
            {
                Enabled = command.CanExecute(null);
                _command.CanExecuteChanged += CommandOnCanExecuteChanged;
            }
        }

        protected override void OnButtonClick(EventArgs e)
        {
            base.OnClick(e);
            if (_command != null && _command.CanExecute(null))
            {
                _command.Execute(null);
            }
        }

        private void CommandOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            if (_command != null)
            {
                Enabled = _command.CanExecute(null);
            }
        }

        #region IBindableComponent

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [ParenthesizePropertyName(true)]
        [Description("The data bindings for the controls.")]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                return _dataBindings = _dataBindings ?? new ControlBindingsCollection(this);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingContext BindingContext
        {
            get
            {
                return _bindingContext = _bindingContext ?? new BindingContext();
            }
            set { _bindingContext = value; }
        }

        #endregion
    }
}
