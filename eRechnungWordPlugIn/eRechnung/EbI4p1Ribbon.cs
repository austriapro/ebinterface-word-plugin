using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using ebIModels.Models;
using ebIViewModels;
using ebIViewModels.RibbonViewModels;
using ebIViewModels.ViewModels;
using eRechnung.Services;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Tools.Word;
using Microsoft.Office.Tools.Word.Extensions;
using Microsoft.Practices.Unity.InterceptionExtension;
using SettingsEditor.ViewModels;
using WinFormsMvvm.Controls;
using SimpleEventBroker;
using ebIViewModels.Services;
using ebIServices;
using LogService;


namespace eRechnung
{
    public partial class EbI4P1Ribbon
    {
        /// <summary>
        /// Alles was mit den Datenfeldern der Rechnung zu tun hat 
        /// </summary>
        private InvoiceViewModel _invoiceView;
        /// <summary>
        /// Alles was mit den Settings zu tun hat
        /// </summary>
        private SettingsViewModel _settingsView;
        /// <summary>
        /// Hilfe, About, etc
        /// </summary>
        private RibbonViewModel _ribbonView;

        // bindung des Ribbonbuttons an ein RibbonCommandButton
        private readonly Dictionary<string, RibbonCommandBinding> _ribbonCommands = new Dictionary<string, RibbonCommandBinding>();

        private void ebI4p1Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            _settingsView = Globals.ThisDocument.SettingsViewModel;
            _ribbonView = Globals.ThisDocument.RibbonViewModel;
            _invoiceView = Globals.ThisDocument.InvoiceViewModel;
            Globals.ThisDocument.InvoiceViewModel.PropertyChanged += InvoiceViewOnPropertyChanged;
            Globals.ThisDocument.InvoiceViewModel.RelatedDoc.PropertyChanged += RelatedViewPropertyChanged;

            Globals.ThisDocument.RegisterSingleEventSubscriber(InvoiceViewModel.InvoiceValidationOptionChanged, OnInvoiceValidationOptionChanged);

            Log.TraceWrite("Ribbon Load");
            Log.TraceWrite("group7 vibility is " + group7.Visible.ToString());
            group7.Visible = true;
            #region Invoice -> InvoiceViewModel
            // RibbonButton btnNewInvoice;
            RegisterCommand(btnNewInvoice, _invoiceView.ClearDocumentCommand);

            // RibbonButton BtnDetails;
            RegisterCommand(BtnDetails, _invoiceView.EditDetailsCommand);

            // RibbonButton BtnSave;
            RegisterCommand(BtnSave, _invoiceView.SaveEbinterfaceCommand);

            // RibbonButton BtnSkonto;
            RegisterCommand(BtnSkonto, _invoiceView.EditSkontoCommand);
            // RibbonButton BtnSaveAsTemplate;
            RegisterCommand(BtnSaveAsTemplate, _invoiceView.SaveTemplateCommand);
            // RibbonButton BtnLoadTemplate;
            RegisterCommand(BtnLoadTemplate, _invoiceView.LoadTemplateCommand);

            // Ribbonbutton BtnVerify
            RegisterCommand(btnVerify, _invoiceView.VerifyCommand);

            // RibbonButton btnSignAndMail;

            // RibbonButton btnMailAndSave;
            RegisterCommand(btnSendByMail, _invoiceView.SaveAndMailButton);
            // RibbonButton btnSendByService;
            RegisterCommand(btnSendByService, _invoiceView.RunZustellDienstButton);
            #endregion
            #region settings -> SettingsViewModel
            RegisterCommand(BtnEditSettings, _settingsView.EditEditRechnungsstellerCommand, true);
            RegisterCommand(gbtnKonto, _settingsView.EditKontoCommandButton);
            RegisterCommand(gbtnHandySignatur, _settingsView.HandySignaturButtonCommand);
            RegisterCommand(gbtnMail, _settingsView.MailButton);
            RegisterCommand(gbtnUidAbfrage, _settingsView.UidAbfrageButton);
            RegisterCommand(gbtnSaveLoc, _settingsView.SaveLocButton);
            RegisterCommand(gbtnZustellung, _settingsView.ZustellgButton);

            #endregion
            #region service -> RibbonViewModel
            // RibbonButton btnUIDBestaetigung;
            RegisterCommand(btnUIDBestaetigung, _ribbonView.UidCheckButton, true);
            // RibbonButton btnVerifySignature;

            // RibbonButton BtnEbInterface;
            RegisterCommand(BtnEbInterface, _ribbonView.EbInterfaceLinkButton);
            // RibbonButton BtnAustriaPro;
            RegisterCommand(BtnAustriaPro, _ribbonView.AustriaProButton);
            // RibbonButton BtnSignatur;
            RegisterCommand(BtnSignatur, _ribbonView.SignaturButton);
            // RibbonButton btnErbGvAt;
            RegisterCommand(btnErbGvAt, _ribbonView.ErbGvAtButton);
            // RibbonButton BtnHelp;
            RegisterCommand(BtnHelp, _ribbonView.HelpButton);
            // RibbonButton btnSupport;
            RegisterCommand(btnSupport, _ribbonView.SupportButton);
            // RibbonButton BtnAbout;
            RegisterCommand(BtnAbout, _ribbonView.BtnAboutCommand, false);
            #endregion

            SetRibbonVisibility(_invoiceView.CurrentSelectedValidation);
            Type word = Type.GetTypeFromProgID("Word.Application.12");
            if (word == null)
            {
                // It is not Word 2007
                this.Base.Ribbon.RibbonUI.ActivateTabMso("TabAddIns");
            }
            Log.TraceWrite("Ribbon Load finished");
        }

        private void RelatedViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RelatedDocumentViewModel relDoc = sender as RelatedDocumentViewModel;
            Log.TraceWrite("entering, property=" + e.PropertyName);
            string prop = e.PropertyName;
            if (prop == "RefDocTypes")
            {
                Globals.ThisDocument.FillRefDocType();
            }
            Log.TraceWrite("exiting");
        }

        private void InvoiceViewOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvoiceViewModel invoice = sender as InvoiceViewModel;
            Log.TraceWrite("entering, property="+e.PropertyName);
            string prop = e.PropertyName;
            if (prop == "CurrentSelectedValidation")
            {
                SetRibbonVisibility(invoice.CurrentSelectedValidation);
            }
            if (prop == "InvTypes")
            {
                Globals.ThisDocument.FillDocType();
            }
            Log.TraceWrite("exiting");
        }

        private void SetRibbonVisibility(InvoiceSubtypes.ValidationRuleSet validation)
        {
            Log.TraceWrite("entering, CurrentSelectedValidation=" + validation.ToString());
            btnSignAndMail.Visible = false;
            BtnSignatur.Visible = false;
            btnVerifySignature.Visible = false;
            switch (validation)
            {
                case InvoiceSubtypes.ValidationRuleSet.Industries:
                    rbGrpSignSend.Visible = true;
                    btnSendByMail.Visible = true;
                    btnSendByService.Visible = true;
                    //  btnSignAndMail.Visible = true;
                    rGrpServices.Visible = true;
                    gbtnZustellung.Visible = true;
                    
                  btnChangeFormType.Label = "Wechsel zu " + _invoiceView.InvoiceVariantList.GetText(InvoiceSubtypes.ValidationRuleSet.Government.ToString());
                    break;
                case InvoiceSubtypes.ValidationRuleSet.Government:
                    rbGrpSignSend.Visible = false;
                    rGrpServices.Visible = false;
                    //   btnSendByMail.Visible = false;
                    btnSendByService.Visible = false;
                    btnSendByMail.Visible = false;
                    btnSignAndMail.Visible = false;
                    gbtnZustellung.Visible = false;
                    
                    btnChangeFormType.Label = "Wechsel zu " + _invoiceView.InvoiceVariantList.GetText(InvoiceSubtypes.ValidationRuleSet.Industries.ToString());
                    break;
                case InvoiceSubtypes.ValidationRuleSet.Invalid:
                    Log.TraceWrite("Ruleset invalid");
                    break;
                default:
                    Log.TraceWrite("selection sot found");
                    throw new ArgumentOutOfRangeException();
            }
            Log.TraceWrite("exiting");

        }

        private void SetEnabled(RibbonButton button, RibbonCommandButton cmd)
        {
            button.Enabled = cmd.CanExecute(null);
        }

        private void SetVisibiblity(RibbonButton button, RibbonCommandButton cmd)
        {
            button.Visible = cmd.IsVisible(null);
        }

        private void RegisterCommand(RibbonButton button, RibbonCommandButton cmd, bool parmRequ = false)
        {
            string msg = "Registering: " + button.Name+", visible="+button.Visible+", parent="+button.Parent.Name;

            Log.TraceWrite(msg);
            
            _ribbonCommands.Add(button.Id, new RibbonCommandBinding()
            {
                Button = button,
                Command = cmd,
                Id = button.Id,
                PropagateInvoice = parmRequ
            });
            button.Click += button_Click;
            cmd.CanExecuteChanged += EnabledChanged;
            cmd.Tag = button;
            cmd.VisibilityChanged += VisibilityChanged;
            button.Enabled = cmd.CanExecute(null);

        }

        private void VisibilityChanged(object sender, EventArgs e)
        {
            RibbonCommandButton cmd = (RibbonCommandButton)sender;
            RibbonButton button = (RibbonButton)cmd.Tag;
            SetVisibiblity(button, cmd);

        }

        private void EnabledChanged(object sender, EventArgs e)
        {
            RibbonCommandButton cmd = (RibbonCommandButton)sender;
            RibbonButton button = (RibbonButton)cmd.Tag;
            SetEnabled(button, cmd);
        }

        private void button_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisDocument.UpdateLastCc();

            if (!_ribbonCommands.ContainsKey(e.Control.Id))
            {
                throw new NotImplementedException(e.Control.Id + " ist nicht registriert.");
            }
            Log.TraceWrite("Button clicked:{0}", _ribbonCommands[e.Control.Id].Button.Name);
            ExecuteRibbonCommandButton(_ribbonCommands[e.Control.Id].Command, _ribbonCommands[e.Control.Id].PropagateInvoice);
        }

        private void ExecuteRibbonCommandButton(RibbonCommandButton cmd, bool parmReq)
        {
            if (cmd != null && cmd.CanExecute(null))
            {
                if (parmReq)
                {
                    cmd.Execute(_invoiceView);
                }
                else
                {
                    cmd.Execute(null);
                }
            }
        }

        [SubscribesTo(InvoiceViewModel.InvoiceValidationOptionChanged)]
        public void OnInvoiceValidationOptionChanged(object sender, EventArgs args)
        {
            InvIndustryEventArgs arg = args as InvIndustryEventArgs;
            Log.TraceWrite("Event: " + InvoiceViewModel.InvoiceValidationOptionChanged);
            SetRibbonVisibility(((InvoiceViewModel)sender).CurrentSelectedValidation);
        }

        private void btnToggleBund_Click(object sender, RibbonControlEventArgs e)
        {
            Log.TraceWrite("Entering, CurrentSelectedValidation=" + Globals.ThisDocument.InvoiceViewModel.CurrentSelectedValidation.ToString());
            if (Globals.ThisDocument.InvoiceViewModel.CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government)
            {
                SetRibbonVisibility(InvoiceSubtypes.ValidationRuleSet.Industries);
                Globals.ThisDocument.InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            }
            else
            {
                SetRibbonVisibility(InvoiceSubtypes.ValidationRuleSet.Government);
                Globals.ThisDocument.InvoiceViewModel.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            }
            //SetRibbonVisibility(Globals.ThisDocument.InvoiceViewModel);
        }
    }
}
