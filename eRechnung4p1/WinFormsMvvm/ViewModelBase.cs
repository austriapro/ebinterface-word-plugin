using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using SimpleEventBroker;
using LogService;
using WinFormsMvvm.DialogService;
using System.Windows.Forms;

namespace WinFormsMvvm
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications 
    /// and has a DisplayName property.  This class is abstract.
    /// See: http://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable, IEditableObject
    {
        #region Constructor
        protected IDialogService _dlg;
        protected ViewModelBase()
        {
           // ChangePending = false;
        }

        protected ViewModelBase(IDialogService dlg)
        {
            _dlg = dlg;
        }
        #endregion // Constructor

        #region DisplayName

        /// <summary>
        /// Returns the user-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _changePending;
        public bool ChangePending {
            get {
                Log.TraceWrite("Changepending={0}", _changePending.ToString());
                return _changePending; 
            }
            set {
                _changePending = value;
                Log.TraceWrite("Changepending={0}", _changePending.ToString());
            } 
        }


        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //  this.VerifyPropertyName(propertyName);

            //Log.LogWrite("entered for " + (propertyName ?? "(null)"));
            Log.TraceWrite("entered for '{0}'", propertyName);
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                ChangePending = true;
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public virtual bool OnFormsClosing(DialogResult resultFromForm, string title)
        {
            bool cancelClose = false;
            if (_dlg == null)
            {
                Log.LogWrite(Log.LogPriority.High, "IDialogService not initialized, Form='{0}'", title);
                return false;
            }
            if (resultFromForm == DialogResult.OK)
            {
                ChangePending = false;
            }
            else
            {
                if (ChangePending==true)
                {
                    DialogResult rc = _dlg.ShowMessageBox("Das Formular enthält möglicherweise Daten, die noch nicht gespeichert wurden. Möchten Sie diese Daten speichern?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (rc == DialogResult.Yes)
                    {
                        cancelClose = true;
                    }
                }
            }
            return cancelClose;

        }

        #endregion // INotifyPropertyChanged Members

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            //string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            //Log.TraceWrite(msg);
        }
#endif

        #endregion // IDisposable Members

        #region Binding specific handling

        private Hashtable props = null;
        // private bool _isEditing = false;

        public void BeginEdit()
        {
            if (props != null)
                return;
            List<PropertyInfo> properties =
                (this.GetType()).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            props = new Hashtable();
            foreach (PropertyInfo info in properties)
            {
                if (info.GetSetMethod() != null)
                {
                    object value = info.GetValue(this, null);
                    props.Add(info.Name, value);
                }
            }
        }

        public void EndEdit()
        {
            props = null;
        }

        public void CancelEdit()
        {
            if (props == null)
                return;
            List<PropertyInfo> properties =
                (this.GetType()).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name != "ChangePending" && info.GetSetMethod() != null)
                {
                    object value = props[info.Name];
                    info.SetValue(this, value, null);
                }
            }
            props = null;
        }

        #endregion

    }
}