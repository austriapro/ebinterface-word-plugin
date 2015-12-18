using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ExtensionMethods;
using WinFormsMvvm;
using WinFormsMvvm.Controls;

namespace WinFormsMvvm.DialogService
{
    public class FormService : Form
    {
        protected object ViewModel;
        protected List<BindingSource> Bindings;
        private Control _currentControl;
        public FormService()
        {
            Load += LoadEvent;
            FormClosing += FormClosingEvent;
            Bindings = new List<BindingSource>();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public virtual void FormClosingEvent(object sender, FormClosingEventArgs e)
        {

            bool cancelClose = ((ViewModelBase)ViewModel).OnFormsClosing(this.DialogResult, this.Text);
            if (cancelClose)
            {
                e.Cancel = true;
            }
            else
            {

                foreach (BindingSource binding in Bindings)
                {
                    if (this.DialogResult == DialogResult.Cancel)
                    {
                        binding.CancelEdit();
                    }
                    else
                    {
                        // WriteAllValues();
                        binding.EndEdit();
                    }
                }
            }

        }

        private void LoadEvent(object sender, EventArgs e)
        {
            // SetupTracking();
        }

        public FormService(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }

        //public DialogResult ShowDialog(ViewModelBase viewModel)
        //{
        //    ViewModel = viewModel;
        //    // SetBindingSource(ViewModel);
        //    return ShowDialog();
        //}

        protected void ExecuteRelayCommand(RelayCommand cmd)
        {
            if (cmd != null && cmd.CanExecute(null))
            {
                cmd.Execute(null);
            }
        }

        private void SetupTracking()
        {
            AddEvent(Controls);
        }

        private void AddEvent(Control.ControlCollection controls)
        {
            if (controls.Count == 0)
            {
                return;
            }
            foreach (Control sender in controls)
            {
                if (sender.Controls.Count > 0)
                    AddEvent(sender.Controls);
                else
                {
                    if (sender is TextBox || sender is MultiColumnComboBox || sender is ComboBox ||
                        sender is DateTimePicker)
                    {
                        sender.Enter += ControlEntering;
                    }
                }
            }

        }

        private void WriteAllValues()
        {
            if (_currentControl == null) return;
            foreach (Binding binding in _currentControl.DataBindings)
            {
                binding.WriteValue();
            }
        }

        public virtual void SetBindingSource(object bindSrc)
        {
            ViewModel = bindSrc;
        }

        private void ControlEntering(object sender, EventArgs e)
        {
            if (sender is TextBox || sender is MultiColumnComboBox || sender is ComboBox || sender is DateTimePicker)
            {
                _currentControl = (Control)sender;
            }
        }

    }
}
