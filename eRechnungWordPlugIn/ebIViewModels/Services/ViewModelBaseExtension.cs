using System;
using System.Runtime.CompilerServices;
using ebIViewModels.ErrorView;
using SimpleEventBroker;
using WinFormsMvvm;
using LogService;
using ebIViewModels.RibbonViewModels;
using Microsoft.Practices.Unity;
using WinFormsMvvm.DialogService;
using ebIViewModels.RibbonViews;

namespace ebIViewModels.Services
{
    public class ViewModelBaseExtension : ViewModelBase
    {
        internal IUnityContainer _uc;

        public ViewModelBaseExtension(IUnityContainer uc, IDialogService dlg)
        {
            _uc = uc;
            _dlg = dlg;
        }

        [Publishes(UpdatePropertyEventArgs.UpdateDropDownSelection)]
        public event EventHandler UpdateDropDownSelectionEvent;

        [Publishes(UpdatePropertyEventArgs.UpdateProtectedProperty)]
        public event EventHandler UpdateProtectedPropertyEvent;

        [Publishes(UpdatePropertyEventArgs.UpdateDocTable)]
        public event EventHandler UpdateDocTableEvent;

        [Publishes(UpdatePropertyEventArgs.ShowPanelEvent)]
        public event EventHandler ShowPanelEvent;

        [Publishes(ErrorActionPaneViewModel.ClearPanelEvent)]
        public event EventHandler ClearPanelEvent;

        [Publishes(ErrorActionPaneViewModel.PublishToPanelEvent)]
        public event EventHandler PublishToPanelEvent;

        public void OnUpdateSelection(string value, [CallerMemberName]string property = null)
        {
            Log.TraceWrite(CallerInfo.Create(),"Property: {0}", property);
            EventHandler handler = UpdateDropDownSelectionEvent;
            if (handler != null)
            {
                UpdatePropertyEventArgs args = new UpdatePropertyEventArgs()
                {
                    Value = value,
                    PropertyName = property
                };
                ChangePending = true;
                handler(this, args);
            }
        }

        public void OnProtectedPropertyChanged(string value, [CallerMemberName] string property = null)
        {
            Log.TraceWrite(CallerInfo.Create(),"Property: {0}", property);
            EventHandler handler = UpdateProtectedPropertyEvent;
            if (handler != null)
            {
                UpdatePropertyEventArgs args = new UpdatePropertyEventArgs()
                {
                    Value = value,
                    PropertyName = property
                };
                ChangePending = true;
                handler(this, args);
            }

        }

        public void OnUpdateDocTable(object value, [CallerMemberName] string property = null)
        {
            Log.TraceWrite(CallerInfo.Create(),"Property: {0}", property);
            EventHandler handler = UpdateDocTableEvent;

            if (handler != null)
            {
                UpdatePropertyEventArgs args = new UpdatePropertyEventArgs()
                {
                    Value = value,
                    PropertyName = property
                };
                ChangePending = true;
                handler(this, args);
            }
        }
        public void PublishToPanel(string description)
        {
            Log.TraceWrite(CallerInfo.Create(),"desc=" + description);
            PublishToPanel("", "", "", description);
        }

        public void PublishToPanel(string field, string description)
        {
            if (field != "")
            {
                Log.TraceWrite(CallerInfo.Create(),"field={0},desc={1}", field, description);
            }
            PublishToPanel("", field, "", description);
        }
        public void PublishToPanel(string message, string field, string description)
        {
            PublishToPanel(message, field, "", description);
        }
        public void PublishToPanel(string message, string field, string severity, string description)
        {
            EventHandler handler = PublishToPanelEvent;
            if (handler != null)
            {
                ErrorPublishSingleEventArgs args = new ErrorPublishSingleEventArgs()
                {
                    Message = message,
                    Description = description,
                    Severity = severity,
                    FieldName = field
                };
                handler(this, args);
            }

        }

        public void ClearPanel()
        {
            EventHandler handler = ClearPanelEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void ShowMessagePanel(bool flag)
        {
            EventHandler handler = ShowPanelEvent;
            if (handler != null)
            {
                UpdatePropertyEventArgs args = new UpdatePropertyEventArgs()
                {
                    Value = flag,
                    PropertyName = "ShowMessagePanel"
                };
                handler(this, args);
            }
        }

    }
}