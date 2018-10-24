using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ebIModels.Models;
using ebIModels.Schema;
using ebIServices.SendMail;
using ebIViewModels;
using ebIViewModels.ErrorView;
using ebIViewModels.RibbonViewModels;
using ebIViewModels.RibbonViews;
using ebIViewModels.Services;
using ebIViewModels.ViewModels;
using ebIViewModels.Views;
using eRechnung.Properties;
using eRechnung.Services;
using eRechnung.Views;
using EventBrokerExtension;
using ExtensionMethods;
using Microsoft.Office.Tools.Word;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using SettingsEditor.ViewModels;
using SettingsEditor.Views;
using SettingsEditor.Service;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using DropDownListViewModels = ebIViewModels.Services.DropDownListViewModels;
using MvvmDialog = WinFormsMvvm.DialogService.FrameworkDialogs;
using Office = Microsoft.Office.Core;

using Word = Microsoft.Office.Interop.Word;
using Tools = Microsoft.Office.Tools;
using System.IO;
using ebIServices;
using LogService;
using System.Drawing;
using Microsoft.Office.Interop.Word;
using System.Threading;




namespace eRechnung
{
    public partial class ThisDocument
    {
        internal static IUnityContainer UContainer { get; set; }
        internal SettingsViewModel SettingsViewModel;
        internal RibbonViewModel RibbonViewModel;
        internal InvoiceViewModel InvoiceViewModel;
        internal ErrorActionsPaneControl ErrorActionsPane;
        internal static IInvoiceModel Invoice;
        internal Dictionary<string, ContentControlContainer> CcContainer = new Dictionary<string, ContentControlContainer>();

        internal IDialogService Dialogs;

        private const string DocDetailsId = "Rabatt%";
        private const string DocMwStId = "MwSt Betrag";
        private const string DocSkontoId = "Skonto Betrag";
        private float redBookmark;
        private const string IsNew = "new";

        [Microsoft.VisualStudio.Tools.Applications.Runtime.Cached()]
        public string CachedString;

        // Zellen für Details
        private enum DetailPos
        {
            DetPos = 1,
            DetBestellPos = 2,
            DetArtNr = 3,
            DetBez = 4,
            DetMenge = 5,
            DetUnit = 6,
            DetUnitPreis = 7,
            DetRabatt = 8,
            DetMwSt = 9,
            DetGesamt = 10
        }

        private enum SkontoPos
        {
            DueDate = 1,
            Tage = 2,
            Prozent = 3,
            BasisBetrag = 4,
            Betrag = 5
        }
        private void ThisDocument_Startup(object sender, System.EventArgs e)
        {
            try
            {
                this.Application.ActiveWindow.View.ReadingLayout = false;
                PlugInSettings.Load();
                UnprotectDocument();
                SetupContainer();
                LockCc();
                SetupAll();
                ProtectPlugIn();

            }
            catch (Exception ex)
            {
                Log.LogWrite(CallerInfo.Create(),Log.LogPriority.High, ex.ToString());
                throw;
            }            
            // System.Windows.Forms.Application.Idle += OnIdle;


        }

        private void OnIdle(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Idle -= OnIdle;

        }

        private void ThisDocument_Shutdown(object sender, System.EventArgs e)
        {
            Log.LogWrite(CallerInfo.Create(),Log.LogPriority.High, "exiting....");
        }

        #region Vom VSTO-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            
            this.CC_InvoiceDocType.Validating += new System.ComponentModel.CancelEventHandler(this.CC_InvoiceDocType_Validating);
            this.CC_InvoiceDocType.Validated += new System.EventHandler(this.CC_InvoiceDocType_Validated);
            this.CC_BillerCountry.Validating += new System.ComponentModel.CancelEventHandler(this.BillerCountry_CC_Validating);
            this.CC_BillerCountry.Validated += new System.EventHandler(this.BillerCountry_CC_Validated);
            this.CC_RecCountry.Validating += new System.ComponentModel.CancelEventHandler(this.ReceipientCountry_CC_Validating);
            this.CC_RecCountry.Validated += new System.EventHandler(this.ReceipientCountry_CC_Validated);
            this.CC_InvCurrency.Validating += new System.ComponentModel.CancelEventHandler(this.CC_InvCurrency_Validating);
            this.CC_InvCurrency.Validated += new System.EventHandler(this.CC_InvCurrency_Validated);
            
            this.CC_RefType.Validating += new System.ComponentModel.CancelEventHandler(this.CC_RefType_Validating);
            this.CC_RefType.Validated += new System.EventHandler(this.CC_RefType_Validated);
            this.CC_RefDocType.Validating += new System.ComponentModel.CancelEventHandler(this.CC_RefDocType_Validating);
            this.CC_RefDocType.Validated += new System.EventHandler(this.CC_RefDocType_Validated);
            this.ContentControlOnExit += new Microsoft.Office.Interop.Word.DocumentEvents2_ContentControlOnExitEventHandler(this.ThisDocument_ContentControlOnExit);
            this.ContentControlOnEnter += new Microsoft.Office.Interop.Word.DocumentEvents2_ContentControlOnEnterEventHandler(this.ThisDocument_ContentControlOnEnter);
            this.Startup += new System.EventHandler(this.ThisDocument_Startup);
            this.Shutdown += new System.EventHandler(this.ThisDocument_Shutdown);
            this.BeforeSave += new Microsoft.Office.Tools.Word.SaveEventHandler(this.ThisDocument_BeforeSave);
            this.New += new Microsoft.Office.Interop.Word.DocumentEvents2_NewEventHandler(this.ThisDocument_New);
            this.Open += new Microsoft.Office.Interop.Word.DocumentEvents2_OpenEventHandler(this.ThisDocument_Open);

        }

        #endregion

        private void SetupAll()
        {
            ResolveModelsAndViews();
            RegisterProtectedProperties();
            AddCcontainerBindings();
            SetupFormatBinding();
            // InvoiceViewModel.Clear();

            //if (CC_BillerName.Text == "")
            //{
            //    CC_BillerName.Range.Select();
            //}
            //else if (string.IsNullOrEmpty(CC_RecName.Text))
            //{
            //    CC_RecName.Range.Select();
            //}
            //else
            //{
            //    CC_InvNr.Range.Select();
            //}
            ThisApplication.ActiveDocument.ActiveWindow.Selection.HomeKey();
#if !DEBUG
            if (string.IsNullOrEmpty(PlugInSettings.Default.Name))
            {
                SettingsViewModel.EditEditRechnungsstellerCommand.Execute(null);
            }
            InvoiceViewModel.OnUpdateFromBillerSettings(null, null);
            var rc = Dialogs.ShowMessageBox("Soll eine eRechnung an die öffentliche Verwaltung erstellt werden?", "Formular wählen",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rc == DialogResult.Yes)
            {
                InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            }
            else
            {
                InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            }
#else
            InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
#endif
        }


        public void LockCc()
        {
            Tools.Word.Document doc = Globals.Factory.GetVstoObject(ThisApplication.ActiveDocument);
            
            Log.TraceWrite(CallerInfo.Create(),"Protection:" + doc.ProtectionType.ToString());
            foreach (Word.ContentControl cc in doc.ContentControls)
            {

                Log.TraceWrite(CallerInfo.Create(),"ID:{0},type:{4},LockContentControl:{1},LockContents:{2},Text:{3},PlaceHolderText:{5}", 
                    cc.ID, cc.LockContentControl, cc.LockContents, cc.Range.Text, cc.Type.ToString(), cc.PlaceholderText.Value);
                bool lockState = cc.LockContents;
                cc.LockContents = false;
                if (cc is RichTextContentControl)
                {
                    ((RichTextContentControl)cc).InnerObject.Range.Font.Hidden = 1;
                }
                else
                {
                    // ToDo: hier müsste auf ShowingPlaceHolderText getestet werden, da beim laden von .docx der Text versteckt wird....
                    cc.Range.Font.Hidden = 1;
                }
                cc.LockContentControl = true;
                cc.LockContents = lockState;
            }
            
            
            var xx = b00.Text;

            foreach (Word.Bookmark bkmk in doc.Bookmarks)
            {
                Log.TraceWrite(CallerInfo.Create(),string.Format("Bookmark Name:{0}", bkmk.Name));

                if (!bkmk.Name.StartsWith("DoNotHide"))
                {
                    bkmk.Range.Font.Hidden = 1;
                }
            }
        }
        private void SetupFormatBinding()
        {
            redBookmark = b27.Font.Size;
            return;
        }

        /// <summary>
        /// Initialisiert den Unity Container
        /// </summary>
        private void SetupContainer()
        {
            UContainer = Register4Unity();
            // Enterprise Block
            //LogWriterFactory = new LogWriterFactory(source);
            //var config = new LoggingConfiguration();

            //UContainer.RegisterInstance<LogWriter>(LogWriterFactory.Create());

            RegisterEventSubscriber();

            Dialogs = UContainer.Resolve<IDialogService>();


            // DropDown Listen füllen
        }

        private void Register4Binding()
        {
            FillDropDownList(CC_BillerCountry,
                InvoiceViewModel.CountryCodeList,
                InvoiceViewModel.VmBillerCountry, "VmBillerCountry", typeof(InvoiceViewModel));

            FillDropDownList(CC_RecCountry,
                InvoiceViewModel.CountryCodeList,
                InvoiceViewModel.VmRecCountry, "VmRecCountry", typeof(InvoiceViewModel));

            FillDropDownList(CC_InvCurrency, InvoiceViewModel.CurrencyList,
                InvoiceViewModel.VmInvCurrency, "VmInvCurrency", typeof(InvoiceViewModel));

            //FillDropDownList(CC_FormValidationType, InvoiceViewModel.InvoiceVariantList,
            //    InvoiceViewModel.CurrentSelectedValidation.ToString(), "CurrentSelectedValidation", typeof(InvoiceViewModel));

            FillDropDownList(CC_InvoiceDocType, InvoiceViewModel.InvTypes,
                InvoiceViewModel.VmDocType, "VmDocType", typeof(InvoiceViewModel));

            FillDropDownList(CC_RefType, InvoiceViewModel.RelatedDoc.RefTypeList,
                InvoiceViewModel.RelatedDoc.RefTypeSelected.ToString(), "RefTypeSelected", typeof(RelatedDocumentViewModel));

            FillDropDownList(CC_RefDocType, InvoiceViewModel.RelatedDoc.RefDocTypes,
                InvoiceViewModel.RelatedDoc.RefSelectedDocType, "RefSelectedDocType", typeof(RelatedDocumentViewModel));
        }

        public void FillDocType()
        {
            UpdateDropDownList(CC_InvoiceDocType, InvoiceViewModel.InvTypes, InvoiceViewModel.VmDocType);
        }
        public void FillRefDocType()
        {
            UpdateDropDownList(CC_RefDocType, InvoiceViewModel.RelatedDoc.RefDocTypes, InvoiceViewModel.RelatedDoc.RefSelectedDocType);
        }
        private void ResolveModelsAndViews()
        {
            ErrorActionsPane = UContainer.Resolve<ErrorActionsPaneControl>();

            this.ActionsPane.Controls.Add(ErrorActionsPane);
            this.CommandBars["Task Pane"].Width = 450;


            SettingsViewModel = UContainer.Resolve<SettingsViewModel>();
            RibbonViewModel = UContainer.Resolve<RibbonViewModel>();
            // MessageBox.Show("CachesString:" + CachedString);

            //if (string.IsNullOrEmpty(CachedString))
            //{
            Invoice.InitFromSettings = true;
            // CachedString = "NotNew";
            // }
            // InvoiceViewModel = UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Invoice));
            InvoiceViewModel = UContainer.Resolve<InvoiceViewModel>();
            invoiceViewModelBindingSource.DataSource = InvoiceViewModel;
            relatedDocBindingSource.DataSource = InvoiceViewModel.RelatedDoc;
            Register4Binding();

        }



        private void AddNullableDateBinding(ContentControlBase ctrl, BindingSource bSrc, Type baseObject, string propertyName, params object[] parms)
        {
            Log.TraceWrite(CallerInfo.Create(),"Adding: " + propertyName);
            AddToContainer(ctrl, baseObject, propertyName);
            var binding = new Binding("Text", bSrc, propertyName, true, DataSourceUpdateMode.OnPropertyChanged, parms);
            binding.Parse += BindingNullableDateParse;
            binding.Format += BindingNullableString;
            ctrl.Exiting += Ctrl_Exiting;
            ctrl.DataBindings.Add(binding);
            Log.TraceWrite(CallerInfo.Create(),"Added: " + propertyName);
        }

        void BindingNullableString(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
                return;
            e.Value = "";
            // SetHidden(((Binding)sender).BindableComponent);

        }

        void BindingNullableDateParse(object sender, ConvertEventArgs e)
        {
            if (string.IsNullOrEmpty((string)e.Value))
            {
                e.Value = new Nullable<DateTime>();
               // SetHidden(((Binding)sender).BindableComponent);
            }

        }
        void BindingTimStringParse(object sender, ConvertEventArgs e)
        {
            if (!string.IsNullOrEmpty((string)e.Value))
            {
                string xTrim = (string)e.Value;
                e.Value = xTrim.Trim();
            }
            else
            {
                e.Value = "";
               // SetHidden(((Binding)sender).BindableComponent);
            }
        }
        // Business Start   
        private void AddDecimalBinding(ContentControlBase ctrl, BindingSource bSrc, Type baseObject, string propertyName, params object[] parms)
        {
            Log.TraceWrite(CallerInfo.Create(),"Adding: " + propertyName);
            AddToContainer(ctrl, baseObject, propertyName);
            var binding = ctrl.DataBindings.Add("Text", bSrc, propertyName, true, DataSourceUpdateMode.OnPropertyChanged,
                 parms);
            if (ctrl is PlainTextContentControl)
            {
                binding.Format += BindingDecimalFormat2;
                binding.Parse += BindingDecimalParse;
            }
            Log.TraceWrite(CallerInfo.Create(),"Added: " + propertyName);
        }

        private void BindingDecimalParse(object sender, ConvertEventArgs e)
        {
            decimal dec = 0;
            if (e.DesiredType == typeof(decimal))
            {
                if (decimal.TryParse((string)e.Value, out dec))
                {
                    e.Value = dec;
                }
                else
                {
                    e.Value = dec;      // Liefert dez. null
                }

            }
        }

        private void BindingDecimalFormat2(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                if (e.Value == null)
                {
                    e.Value = "0,00";
                }
                else
                {
                    e.Value = ((decimal)e.Value).Decimal2();
                }
            }
        }
        // Business End

        private void AddBinding(ContentControlBase ctrl, BindingSource bSrc, Type baseObject, string propertyName, params object[] parms)
        {
            Log.TraceWrite(CallerInfo.Create(),"Adding: " + propertyName);
            AddToContainer(ctrl, baseObject, propertyName);
            var binding = new Binding("Text", bSrc, propertyName, true, DataSourceUpdateMode.OnValidation, // DataSourceUpdateMode.OnPropertyChanged,
                 parms);
            if (ctrl is PlainTextContentControl)
            {
                binding.Format += BindingNullableString;
                binding.Parse += BindingTimStringParse;
               // ctrl.Exiting += ctrl_Exiting;
            }
                
           
            //if (ctrl is DateTimePicker)
            //{
            //   // ctrl.Exiting += ctrl_Exiting;
            //}
            
            ctrl.DataBindings.Add(binding);
            Log.TraceWrite(CallerInfo.Create(),"Added: " + propertyName);
        }

        void Ctrl_Exiting(object sender, ContentControlExitingEventArgs e)
        {
            Log.TraceWrite(CallerInfo.Create(),"set hidden: ", ((ContentControlBase)sender).ID);
            SetHidden(sender);
        }

        private static void SetHidden(object sender)
        {
            if (sender is PlainTextContentControl)
            {
                var pctrl = (PlainTextContentControl)sender;
                if (pctrl.ShowingPlaceholderText)
                {
                    pctrl.Range.Font.Hidden = 1;
                }
            }
            if (sender is DatePickerContentControl)
            {
                var pctrl = (DatePickerContentControl)sender;
                if (pctrl.ShowingPlaceholderText)
                {
                    pctrl.Range.Font.Hidden = 1;
                }

            }
            if (sender is RichTextContentControl)
            {
                var pctrl = (RichTextContentControl)sender;
                pctrl.InnerObject.Range.Font.Hidden = 1;
            }
        }

        private void AddToContainer(ContentControlBase ctrl, Type baseObject, string propertyName)
        {
            ContentControlContainer cc = new ContentControlContainer()
            {
                CcControl = ctrl,
                DownListView = null,
                VmProperty = propertyName,
                TargetType = baseObject
            };
            CcContainer.Add(propertyName, cc);
        }

        private void AddCcontainerBindings()
        {

            AddBinding(CC_BillerName, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerName");
            AddBinding(CC_BillerStreet, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerStreet");
            AddBinding(CC_BillerPlz, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerPlz");
            AddBinding(CC_BillerTown, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerTown");
            AddBinding(CC_BillerGln, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerGln", string.Empty);
            AddBinding(CC_BillerContact, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerContact");
            AddBinding(CC_BillerPhone, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerphone");
            AddBinding(CC_BillerMail, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerMail");
            AddBinding(CC_BillerVatID, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmBillerVatid");
            AddBinding(CC_RecSalutation, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecSalutation");
            AddBinding(CC_RecName, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecName", string.Empty);
            AddBinding(CC_RecStreet, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecStreet");
            AddBinding(CC_RecPlz, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecPlz");
            AddBinding(CC_RecTown, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecTown");
            AddBinding(CC_RecGln, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecGln", string.Empty);
            AddBinding(CC_RecContact, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecContact");
            AddBinding(CC_RecPhone, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecPhone");
            AddBinding(CC_RecMail, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecMail");
            AddBinding(CC_RecVatID, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmRecVatid");
            AddBinding(CC_InTitle, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmInvTitle");
            AddBinding(CC_LiefantenNr, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmLiefantenNr");
            AddBinding(CC_KundenNr, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKundenNr");
            AddBinding(CC_InvNr, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmInvNr");
            AddBinding(CC_InvDate, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmInvDate", string.Empty, "d");
            AddBinding(CC_OrderReference, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmOrderReference");
            AddNullableDateBinding(CC_OrderDate, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmOrderDate", null, "d");
            AddBinding(CC_SubOrganisation, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmSubOrganisation");
            AddBinding(CC_AcctArea, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmAcctArea");
            AddNullableDateBinding(CC_LieferDatum, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmLieferDatum", "", "d");
            AddNullableDateBinding(CC_InvDueDate, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmInvDueDate", "", "d");
            AddBinding(CC_DocComment, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmComment", "");
            AddBinding(CC_Kopftext, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKopfText", "");
            AddBinding(CC_Fusstext, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmFussText");
            AddBinding(CC_KtoBankName, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKtoBankName");
            AddBinding(CC_KtoBic, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKtoBic");
            AddBinding(CC_KtoIBAN, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKtoIban");
            AddBinding(CC_KtoOwner, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKtoOwner");
            AddBinding(CC_KtoReference, invoiceViewModelBindingSource, typeof(InvoiceViewModel), "VmKtoReference");
            AddBinding(CC_RefInvNr, relatedDocBindingSource, typeof(RelatedDocumentViewModel), "RefInvNumber");
            AddNullableDateBinding(CC_RefInvDate, relatedDocBindingSource, typeof(RelatedDocumentViewModel), "RefInvDate", string.Empty, "d");
            AddBinding(CC_RefComment, relatedDocBindingSource, typeof(RelatedDocumentViewModel), "RefComment");
        }

        private void RegisterProtectedProperties()
        {
            RegisterContentControl(CC_InvTotalAmountText, null, "VmInvTotalAmountText", InvoiceViewModel.VmInvTotalAmountText, typeof(InvoiceViewModel));
            RegisterContentControl(CC_InvTotalNetAmountText, null, "VmInvTotalNetAmountText", InvoiceViewModel.VmInvTotalNetAmountText, typeof(InvoiceViewModel));
            RegisterContentControl(CC_InvTaxText, null, "VmInvTaxText", InvoiceViewModel.VmInvTaxText, typeof(InvoiceViewModel));
            RegisterContentControl(CC_InvTotalAmount, null, "VmInvTotalAmount", InvoiceViewModel.VmInvTotalAmount, typeof(InvoiceViewModel));
            RegisterContentControl(CC_InvTotalNetAmount, null, "VmInvTotalNetAmount", InvoiceViewModel.VmInvTotalNetAmount, typeof(InvoiceViewModel));
            RegisterContentControl(CC_InvTaxAmount, null, "VmInvTaxAmount", InvoiceViewModel.VmInvTaxAmount, typeof(InvoiceViewModel));
            RegisterContentControl(CC_DocRef, null, "VmDocRef", InvoiceViewModel.VmDocRef, typeof(InvoiceViewModel));
        }

        private void RegisterContentControl(ContentControlBase ddCtrl, DropDownListViewModels dropdown, string propertyName, string value, Type targetType)
        {
            CcContainer.Add(propertyName, new ContentControlContainer()
            {
                CcControl = ddCtrl,
                DownListView = dropdown,
                VmProperty = propertyName,
                TargetType = targetType
            });
            if (dropdown == null)
            {
                PlainTextContentControl ctrl = ((PlainTextContentControl)ddCtrl);
                bool saveLockState = ctrl.LockContents;
                ctrl.LockContents = false;
                ctrl.Text = value;
                ctrl.LockContents = saveLockState;
            }
        }

        /// <summary>
        /// Füllt eine DropDownContentControl
        /// </summary>
        /// <param name="ddCtrl"></param>
        /// <param name="dropdown"></param>
        /// <param name="toSelect"></param>
        /// <param name="propertyName"></param>
        /// <param name="targetType"></param>
        private void FillDropDownList(DropDownListContentControl ddCtrl, DropDownListViewModels dropdown, string toSelect, string propertyName, Type targetType)
        {
            Log.TraceWrite(CallerInfo.Create(),"Registering: " + propertyName + string.Format(" adding {0} elements.", dropdown.DropDownList.Count));
            RegisterContentControl(ddCtrl, dropdown, propertyName, null, targetType);
            UpdateDropDownList(ddCtrl, dropdown, toSelect);
        }

        private void UpdateDropDownList(DropDownListContentControl ddCtrl, DropDownListViewModels dropdown, string toSelect)
        {
            ddCtrl.DropDownListEntries.Clear();
            foreach (var dropDownList in dropdown.DropDownList)
            {
                ddCtrl.DropDownListEntries.Add(dropDownList.DisplayText, dropDownList.Code);
            }
            SelectDropDownValue(ddCtrl.DropDownListEntries, toSelect);
        }

        /// <summary>
        /// Selektiert einen Wert in der ContentControlList
        /// </summary>
        /// <param name="myLe"></param>
        /// <param name="wert"></param>
        private void SelectDropDownValue(Word.ContentControlListEntries myLe, string wert)
        {
            if (string.IsNullOrEmpty(wert))
            {
                return;
            }
            foreach (Word.ContentControlListEntry myEntry in myLe)
            {
                if (myEntry.Value == wert)
                {
                    myEntry.Select();
                }
            }
        }

        private string GetSelectedValue(DropDownListViewModels dropDownList, string selectedText)
        {
            var findCode = dropDownList.DropDownList.Where(text => text.DisplayText == selectedText).Select(text => text.Code);
            if (findCode.Any())
            {
                return findCode.FirstOrDefault();
            }
            return null;
        }

        #region Event Subscriber

        [SubscribesTo(InvoiceViewModel.InvoiceValidationOptionChanged)]
        public void UpdateBookmarks(object sender, EventArgs args)
        {
            InvIndustryEventArgs arg = args as InvIndustryEventArgs;
            Log.TraceWrite(CallerInfo.Create(),"@entry, switch to Industry {0}", arg.Industry);
            UnprotectPlugIn();
            try
            {
            string formText = InvoiceViewModel.InvoiceVariantList.GetText(arg.Industry.ToString());
            Log.TraceWrite(CallerInfo.Create(),"Text={0}", formText);
            Log.TraceWrite(CallerInfo.Create(),"bkmk={0}, id={1}", bkmkFormular.Name,bkmkFormular.ID);
            bkmkFormular.Text = formText;
            if (arg != null)
                switch (arg.Industry)
                {
                    case InvoiceSubtypes.ValidationRuleSet.Industries:
                            SetBkmkFontSize(bestpos, 1);
                            SetBkmkFontSize(b27, 1);
                            SetBkmkFontSize(bLiefNr, 1);
                            SetBkmkFontSize(bEmpfMail, 1); // MUss beim e-Mail Versand geprüft werden
                        break;
                    case InvoiceSubtypes.ValidationRuleSet.Government:
                            SetBkmkFontSize(bestpos, redBookmark);
                            SetBkmkFontSize(b27, redBookmark);
                            SetBkmkFontSize(bLiefNr, redBookmark);
                            SetBkmkFontSize(bEmpfMail, redBookmark);
                            SetBkmkFontSize(bEmpfMail, 1);
                        break;
                    default:
                        // throw new ArgumentOutOfRangeException();
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.LogWrite(CallerInfo.Create(),Log.LogPriority.High, ex.Message);
                Log.LogWrite(CallerInfo.Create(),Log.LogPriority.High, ex.StackTrace);
            } 
            ProtectPlugIn();
        }
        private void SetBkmkFontSize(Tools.Word.Bookmark bkmk, float size)
        {
            Log.TraceWrite(CallerInfo.Create(),"Bookmark={0}, Size={1}", bkmk.Name, size);
            bkmk.Font.Size = size;
        }
        [SubscribesTo(UpdatePropertyEventArgs.ShowPanelEvent)]
        public void ShowErrorPane(object sender, EventArgs args)
        {
            UpdatePropertyEventArgs arg = args as UpdatePropertyEventArgs;
            bool flag = (bool)arg.Value;

            Views.ErrorActionsPaneControl errorActions = ErrorActionsPane; // crazy story??
            if (!flag)
            {
                this.ActionsPane.Controls.Remove(errorActions);
                this.ActionsPane.Clear();
            }
            else
            {
                if (!this.ActionsPane.Contains(errorActions))
                {
                    this.ActionsPane.Controls.Add(errorActions);
                }
                // this.ActionsPane.Show();
                // ActionsPane.Visible = true;
                this.Application.TaskPanes[Word.WdTaskPanes.wdTaskPaneDocumentActions].Visible = true;
            }
            errorActions.Visible = flag;
        }

        [SubscribesTo(UpdatePropertyEventArgs.UpdateProtectedProperty)]
        public void UpdateProtectedProperty(object sender, EventArgs args)
        {
            UpdatePropertyEventArgs arg = args as UpdatePropertyEventArgs;
            if (!CcContainer.ContainsKey(arg.PropertyName))
            {
                throw new ArgumentNullException(arg.PropertyName + " not registered in CcContainer.");
            }
            UnprotectPlugIn();
            ContentControlContainer ccEntry = CcContainer[arg.PropertyName];
            PlainTextContentControl plainTextCc = (PlainTextContentControl)ccEntry.CcControl;
            Log.TraceWrite(CallerInfo.Create(),"Update Protected Property: '{0}', Property:{1}, Value='{2}'", plainTextCc.Title, arg.PropertyName, ((string)arg.Value));
            plainTextCc.LockContents = false;
            plainTextCc.Text = (string)arg.Value;
            plainTextCc.LockContents = true;
            ProtectPlugIn();
        }

        [SubscribesTo(UpdatePropertyEventArgs.UpdateDropDownSelection)]
        public void SetDropDownSelection(object sender, EventArgs args)
        {
            UpdatePropertyEventArgs arg =
                args as UpdatePropertyEventArgs;
            if (!CcContainer.ContainsKey(arg.PropertyName))
            {
                throw new ArgumentNullException(arg.PropertyName + " not registered in CcContainer.");
            }
            ContentControlContainer ddEntry = CcContainer[arg.PropertyName];
            DropDownListContentControl ddCtrl = (DropDownListContentControl)ddEntry.CcControl;
            SelectDropDownValue(ddCtrl.DropDownListEntries, (string)arg.Value);

        }

        [SubscribesTo(UpdatePropertyEventArgs.UpdateDocTable)]
        public void UpdateDocTable(object sender, EventArgs args)
        {
            UpdatePropertyEventArgs arg = args as UpdatePropertyEventArgs;
            Log.TraceWrite(CallerInfo.Create(),"entering for {0}", arg.PropertyName);
            if (arg != null)
            {
                UnprotectDocument();

                //Application.ScreenUpdating = false;

                switch (arg.PropertyName)
                {
                    case "DetailsView":
                        UpdateDetailsTable(arg);
                        break;
                    case "VatView":
                        UpdateVatTable(arg);
                        break;
                    case "PaymentConditions":
                        UpdateSkontoTable(arg);
                        break;
                    default:
#if DEBUG
                        //   Application.ScreenUpdating = true;
                        //  throw new ArgumentException("Unknown property " + arg.PropertyName);
#endif
                        break;
                }

                //Application.ScreenUpdating = true;
                //Application.ScreenRefresh();
                ProtectPlugIn();
                Log.TraceWrite(CallerInfo.Create(),"exiting");
            }
        }


        [SubscribesTo(InvoiceViewModel.DocumentHomeKey)]
        public void OnDocumentHomeKey(object sender, EventArgs args)
        {
            CC_RecSalutation.Range.Select();
        }

        [SubscribesTo(InvoiceViewModel.SaveAsPdfEvent)]
        public void OnSaveAsPdfEvent(object sender, EventArgs args)
        {
            SaveAsPdfAndSendMailEventArgs arg = args as SaveAsPdfAndSendMailEventArgs;
            // SaveDocumentAsPdf(pdfFileName);
            Word.Document doc = Globals.ThisDocument.Application.ActiveDocument;
            SaveDocumentAsPdf2(ref doc, arg.PdfFilename);
        }

        [SubscribesTo(InvoiceViewModel.SendMailEvent)]
        public void OnSendMailEvent(object sender, EventArgs args)
        {
            SaveAsPdfAndSendMailEventArgs arg = args as SaveAsPdfAndSendMailEventArgs;
            //// SaveDocumentAsPdf(pdfFileName);
            //Word.Document doc = Globals.ThisDocument.Application.ActiveDocument;
            //SaveDocumentAsPdf2(ref doc, arg.PdfFilename);
            // OnSaveAsPdfEvent(sender, args);

            ISendMailService mailService = UContainer.Resolve<ISendMailService>();
            mailService.MailBody = arg.MailBody;
            mailService.PdfFileName = arg.PdfFilename;
            mailService.XmlFilename = arg.XmlFilename;
            mailService.SendTo = arg.SendTo;
            mailService.Subject = arg.Subject;
            mailService.SendMail();
        }

        private void UpdateSkontoTable(UpdatePropertyEventArgs arg)
        {
            SkontoViewModels skontoView = (SkontoViewModels)arg.Value;
            List<SkontoViewModel> skontoList = new List<SkontoViewModel>(skontoView.SkontoList); //skontoView.SkontoList.ToList();
            Word.Table skontoTable = FindTable(DocSkontoId);
            DeleteAllRows(skontoTable, 0);
            if (skontoList.Count == 0)
            {
                return;
            }
            // Word.Row cRow = skontoTable.Rows[2];
            for (int i = 0; i < skontoList.Count-1; i++)
            {
                Log.TraceWrite(CallerInfo.Create(),"Add row# {0}", i + 1);
                skontoTable.Rows.Add();
            }
            int newRows = skontoList.Count - 1;
            int iRow = 2;
            foreach (SkontoViewModel skonto in skontoList)
            {
                //cRow.Select();
                //Word.Cells cells = cRow.Cells;

                FillCell(skontoTable.Cell(iRow,(int)SkontoPos.DueDate), skonto.SkontoFaelligDate.ToString("dd.MM.yyyy"));
                FillCell(skontoTable.Cell(iRow, (int)SkontoPos.Tage), skonto.SkontoTage.ToString("0"));
                FillCell(skontoTable.Cell(iRow, (int)SkontoPos.Prozent), skonto.SkontoProzent.Percent2());
                if (skonto.SkontoBasisBetrag > 0)
                {
                    FillCell(skontoTable.Cell(iRow, (int)SkontoPos.BasisBetrag), skonto.SkontoBasisBetrag.Decimal2());
                }
                FillCell(skontoTable.Cell(iRow, (int)SkontoPos.Betrag), skonto.SkontoBetrag.Decimal2());
                // Application.ScreenRefresh();
                // Thread.Sleep(0);
                newRows--;
                iRow++;
                if (newRows < 0)
                    break;
                // cRow = skontoTable.Rows.Add();
            }
            // cRow.Delete();
            Log.TraceWrite(CallerInfo.Create(),"finished.");
        }

        private void UpdateDetailsTable(UpdatePropertyEventArgs arg)
        {
            Log.TraceWrite(CallerInfo.Create(),"entering");
            // DetailsListViewModel det = (DetailsListViewModel)arg.Value;
            // BindingList<DetailsViewModel> bindDetails = (BindingList<DetailsViewModel>)arg.Value;
            var details = new List<DetailsViewModel>((BindingList<DetailsViewModel>)arg.Value);
            var delTab = FindTable(DocDetailsId);
            DeleteAllRows(delTab, 0);
            if (details.Count < 1)
                return;
            int iRow = 2;
            Word.Table detailsTab = FindTable(DocDetailsId);
            //Word.Row cRow = detailsTab.Rows[iRow];
            Word.Rows rows = detailsTab.Rows;
            Log.TraceWrite(CallerInfo.Create(),"Anzahl Zeilen vor Update: {0}", rows.Count);
            // UnprotectPlugIn();
            detailsTab.Select();
           // var xx = 
            for (int i = 0; i < details.Count - 1; i++)
            {
                Log.TraceWrite(CallerInfo.Create(),"Add row# {0}", i + 1);

                // detailsTab.Rows.Add();

                var xx = rows.Add(rows[2]);
                Log.TraceWrite(CallerInfo.Create(),"Added {0} cells", xx.Cells.Count);

            }
            int newRows = details.Count - 1;
            // cRow = detailsTab.Rows[iRow];
            foreach (DetailsViewModel detail in details)
            {
                Log.TraceWrite(CallerInfo.Create(),"Processing row {0}", iRow);
                // cRow.Select();
                // Word.Cells cells = cRow.Cells;
                string pos = (iRow - 1).ToString();
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetPos), pos); // Positionsnr
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetBestellPos), detail.BestellBezug);
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetArtNr), detail.ArtikelNr);
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetBez), detail.Bezeichnung);
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetMenge), detail.Menge.Decimal4());
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetUnit), detail.EinheitDisplay);
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetUnitPreis), detail.EinzelPreis.Decimal4());
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetRabatt), (detail.Rabatt).Percent2());
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetMwSt), detail.VatItem.MwStSatz.Percent2());
                FillCell(detailsTab.Cell(iRow, (int)DetailPos.DetGesamt), detail.NettoBetragZeile.Decimal2());
                newRows--;
                if (newRows < 0)
                    break;
                // Hier ist das Problem. Word 2007 
                //Log.TraceWrite(CallerInfo.Create(),"vor Add");
                //cRow = detailsTab.Rows.Add(cRow);
                //Log.TraceWrite(CallerInfo.Create(),"nach Add");
                iRow++;
            }

            // cRow.Delete();
            Log.TraceWrite(CallerInfo.Create(),"finished");

        }

        private void UpdateVatTable(UpdatePropertyEventArgs arg)
        {
            VatViewModels vatView = (VatViewModels)arg.Value;
            List<VatViewModel> vatViewModels = vatView.VatViewList;

            // Vorhandene Tabellezeilen löschen
            Word.Table vatTable = FindTable(DocMwStId);
            DeleteAllRows(vatTable, 3);
            if (vatViewModels.Count < 1)
                return;
            int iRow = 2; // Startzeile in der Tabelle
            Word.Row cRow = vatTable.Rows[iRow];
            int newRows = vatViewModels.Count - 1;
            foreach (VatViewModel viewModel in vatViewModels)
            {
                cRow.Select();
                Word.Cells cells = cRow.Cells;
                FillCell(cells[1], viewModel.VatPercent.Percent2()); // MwSt Satz
                FillCell(cells[2], viewModel.VatBaseAmount.Decimal2()); // Basisbetrag
                FillCell(cells[3], viewModel.VatAmount.Decimal2()); // MwSt Betrag
                FillCell(cells[4], viewModel.VatTotalAmount.Decimal2()); // Gesamt betrag
                //  Application.ScreenRefresh();
                // Thread.Sleep(0);
                newRows--;
                if (newRows < 0)
                    break;
                var lastRow = cRow;
                cRow = vatTable.Rows.Add(lastRow); // neue Zeile anfügen
            }
            // cRow.Delete();
        }

        private void FillCell(Word.Cell cell, string text)
        {
            Word.Cell c = cell;
            Log.TraceWrite(CallerInfo.Create(),"cell: {0}, text: {1}", c.ColumnIndex, text);
            c.Range.Text = "";
            if (!string.IsNullOrEmpty(text))
            {
                c.Range.Text = text.Trim();
            }
        }
        private Word.Table FindTable(string searchText)
        {
            Log.TraceWrite(CallerInfo.Create(),"entering for {0}", searchText);
            var requiredTable = Tables.Cast<Word.Table>().FirstOrDefault(t => t.Range.Text.Contains(searchText));
            Log.TraceWrite(CallerInfo.Create(),requiredTable.Rows[1].Range.Text);
            return requiredTable;
        }

        private void DeleteAllRows(Word.Table tab, int keepRows)
        {
            int tabRows = tab.Rows.Count;
            Log.TraceWrite(CallerInfo.Create(),"entering, keepRows:{0}, Row Count:{1}", keepRows, tabRows);
            int totalRowsToRemove = tabRows - keepRows;
            for (int i = totalRowsToRemove; i > 2; i--)
            {
                tab.Rows[i].Delete();
            }
            Word.Row cRow = tab.Rows[2];
            foreach (Word.Cell cell in cRow.Cells)
            {
                cell.Range.Text = "";
            }
            Log.TraceWrite(CallerInfo.Create(),"exiting");
        }
        #endregion

        #region Validation
        private void BillerCountry_CC_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false; // Always return OK, so that validated event is fireing
        }

        private void BillerCountry_CC_Validated(object sender, EventArgs e)
        {

            InvoiceViewModel.VmBillerCountry =
                GetSelectedValue(InvoiceViewModel.CountryCodeList, CC_BillerCountry.Text);

        }

        private void ReceipientCountry_CC_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void ReceipientCountry_CC_Validated(object sender, EventArgs e)
        {
            InvoiceViewModel.VmRecCountry =
                GetSelectedValue(InvoiceViewModel.CountryCodeList, CC_RecCountry.Text);
        }

        private void CC_InvCurrency_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void CC_InvCurrency_Validated(object sender, EventArgs e)
        {
            InvoiceViewModel.VmInvCurrency = Enum.Parse(typeof(CurrencyType), CC_InvCurrency.Text).ToString();
        }

        //private void CC_FormValidationType_Validating(object sender, CancelEventArgs e)
        //{
        //    Log.LogWrite(CallerInfo.Create(),"entering");
        //    e.Cancel = false;
        //}

        //private void CC_FormValidationType_Validated(object sender, EventArgs e)
        //{
        //    Log.LogWrite(CallerInfo.Create(),"entering, old=" + InvoiceViewModel.CurrentSelectedValidation.ToString() + ", new=" + CC_FormValidationType.Text);
        //    InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.GetVariant(CC_FormValidationType.Text);
        //    Log.LogWrite(CallerInfo.Create(),"exiting, old=" + InvoiceViewModel.CurrentSelectedValidation.ToString() + ", new=" + CC_FormValidationType.Text);
        //}

        private void CC_InvoiceDocType_Validating(object sender, CancelEventArgs e)
        {
            string txt = CC_InvoiceDocType.Text;
            var sel = from c in InvoiceViewModel.InvTypes.DropDownList
                      where c.DisplayText == txt
                      select c;
            if (!sel.Any())
            {
                Dialogs.ShowMessageBox("Dokumentenart " + CC_InvoiceDocType + " für " + bkmkFormular + " ungültig.", "Validierung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

        }

        private void CC_InvoiceDocType_Validated(object sender, EventArgs e)
        {
            string sel = GetSelectedValue(InvoiceViewModel.InvTypes, CC_InvoiceDocType.Text);
            InvoiceViewModel.VmDocType = sel;
        }

        private void CC_RefDocType_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void CC_RefDocType_Validated(object sender, EventArgs e)
        {
            string sel = GetSelectedValue(InvoiceViewModel.InvTypes, CC_RefDocType.Text);
            InvoiceViewModel.RelatedDoc.RefSelectedDocType = sel;
        }

        private void CC_RefType_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void CC_RefType_Validated(object sender, EventArgs e)
        {
            string sel = GetSelectedValue(InvoiceViewModel.RelatedDoc.RefTypeList, CC_RefType.Text);
            InvoiceViewModel.RelatedDoc.RefTypeSelected = (RelatedDocumentViewModel.RefType)Enum.Parse(typeof(RelatedDocumentViewModel.RefType), sel);
        }
        #endregion
        #region Protection

        private void ProtectPlugIn()
        {
            object noReset = false;
            object password = System.String.Empty;
            object useIRM = false;
            object enforceStyleLock = false;
            Log.TraceWrite(CallerInfo.Create(),"Protect: Status={0}", this.ProtectionType.ToString());
            if (ProtectionType == Word.WdProtectionType.wdNoProtection)
            {
                this.Protect(Word.WdProtectionType.wdAllowOnlyFormFields,
                    ref noReset, ref _pwd, ref useIRM, ref enforceStyleLock);
            }
        }

        private void UnprotectPlugIn()
        {
            Log.TraceWrite(CallerInfo.Create(),"Unprotect: Status={0}", this.ProtectionType.ToString());

            if (ProtectionType != Word.WdProtectionType.wdNoProtection)
            {
                this.Unprotect(ref _pwd);
            }
        }

        object _pwd = "ebInterface4.1";

        protected override void ProtectDocument()
        {
            Log.TraceWrite(CallerInfo.Create(),"Protect: Status={0}", this.ProtectionType.ToString());
                ProtectPlugIn();
            }

        protected override void UnprotectDocument()
        {
            Log.TraceWrite(CallerInfo.Create(),"Unprotect: Status={0}", this.ProtectionType.ToString());
            UnprotectPlugIn();
        }
        public int GetOfficeVersion()
        {
            int sVersion = 0;
            Microsoft.Office.Interop.Word.Application appVersion = new Microsoft.Office.Interop.Word.Application
            {
                Visible = false
            };

            switch (appVersion.Version.ToString())
            {
                //case "12.0":
                //    sVersion = 2007;
                //    break;
                case "14.0":
                    sVersion = 2010;
                    break;
                case "15.0":
                    sVersion = 2013;
                    break;
                default:
                    sVersion = 0;
                    break;
            }
            return sVersion;
        }


        #endregion
        public void SaveDocumentAsPdf2(ref Word.Document document, string targetPath)
        {
            Word.WdExportFormat exportFormat = Word.WdExportFormat.wdExportFormatPDF;
            bool openAfterExport = false;
            Word.WdExportOptimizeFor exportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
            Word.WdExportRange exportRange = Word.WdExportRange.wdExportAllDocument;
            int startPage = 0;
            int endPage = 0;
            Word.WdExportItem exportItem = Word.WdExportItem.wdExportDocumentContent;
            bool includeDocProps = false;
            bool keepIRM = true;
            Word.WdExportCreateBookmarks createBookmarks = Word.WdExportCreateBookmarks.wdExportCreateNoBookmarks;
            bool docStructureTags = true;
            bool bitmapMissingFonts = true;
            bool useISO19005_1 = false;
            object missing = Missing.Value;

            // Export it in the specified format.  
            if (document != null)
                UnprotectPlugIn();
            try
            {
                document.ExportAsFixedFormat(
                    targetPath,
                    exportFormat,
                    openAfterExport,
                    exportOptimizeFor,
                    exportRange,
                    startPage,
                    endPage,
                    exportItem,
                    includeDocProps,
                    keepIRM,
                    createBookmarks,
                    docStructureTags,
                    bitmapMissingFonts,
                    useISO19005_1,
                    ref missing);
            }
            finally
            {
                ProtectPlugIn();
            }

        }
        public void UpdateLastCc()
        {
            if (LastCc == null)
            {
                return;
            }
            if (LastCc.ShowingPlaceholderText)
            {
                return;
            }
            if (!CcContainer.Select(p => p.Value.CcControl.ID == LastCc.ID).Any())
            {
                throw new KeyNotFoundException("Control not registered: " + LastCc.ID);
            }
            var cc = CcContainer.First(p => p.Value.CcControl.ID == LastCc.ID);

            var container = cc.Value;
            object tgt;
            if (container.TargetType == typeof(Bitmap))
            {
                return;
            }
            if (container.TargetType == typeof(InvoiceViewModel))
            {
                tgt = InvoiceViewModel;
            }
            else
            {
                if (container.TargetType == typeof(RelatedDocumentViewModel))
                {
                    tgt = InvoiceViewModel.RelatedDoc;
                }
                else
                {
                    throw new NotImplementedException("Handling not implemented for type " + container.TargetType.FullName);
                }
            }
            Type typ = tgt.GetType();
            PropertyInfo prop = typ.GetProperty(cc.Value.VmProperty);
            if (prop.PropertyType == typeof(string))
            {
                string val = LastCc.Range.Text;
                if (container.DownListView != null)
                {
                    val = GetSelectedValue(container.DownListView, val);
                }
                string oldVal = (string)prop.GetValue(tgt);
                if (oldVal == val)
                    return;
                prop.SetValue(tgt, val, null);
            }
            else
            {
                if (prop.PropertyType.BaseType == typeof(Enum))
                {

                    string valInCc = LastCc.Range.Text;
                    string val = GetSelectedValue(container.DownListView, valInCc);
                    string oldVal = prop.GetValue(tgt).ToString();
                    if (oldVal == val)
                        return;
                    var newVal = Enum.Parse(prop.PropertyType, val);
                    prop.SetValue(tgt, newVal, null);
                }
                else
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        string val = LastCc.Range.Text;
                        DateTime valDate = DateTime.Parse(val);
                        DateTime oldVal = (DateTime)prop.GetValue(tgt);
                        if (valDate.CompareTo(oldVal) == 0)
                        {
                            return;
                        }
                        prop.SetValue(tgt, valDate, null);
                    }
                }
            }
        }
        public Word.ContentControl LastCc = null;
        private void ThisDocument_ContentControlOnEnter(Word.ContentControl contentControl)
        {
            LastCc = null;
            LastCc = contentControl;

        }

        private void ThisDocument_ContentControlOnExit(Word.ContentControl ContentControl, ref bool Cancel)
        {
            LastCc = null;
        }

        private void ThisDocument_BeforeSave(object sender, SaveEventArgs e)
        {
            Log.TraceWrite(CallerInfo.Create(),"at Entry");
            // var rc = Dialogs.ShowMessageBox("Die eRechnung ")
            // this.InvoiceViewModel.SaveEbinterfaceCommand.Execute(null);
            // UnprotectDocument();
            //e.Cancel = true;
           
        }

        private void ThisDocument_New()
        {
            Log.TraceWrite(CallerInfo.Create(),"at Entry");
            //   MessageBox.Show("new");
            CachedString = "";
            // InvoiceViewModel.Clear();
        }

        private void ThisDocument_Open()
        {
            Log.TraceWrite(CallerInfo.Create(),"at Entry");
            //     MessageBox.Show("Old");
            this.Application.ActiveWindow.View.ReadingLayout = false;
            //  CachedString = "Old";
        }

    }
}
