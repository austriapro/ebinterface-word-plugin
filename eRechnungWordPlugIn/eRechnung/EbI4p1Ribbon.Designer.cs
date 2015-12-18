namespace eRechnung
{
    partial class EbI4P1Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EbI4P1Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">"true", wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls "false".</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.group7 = this.Factory.CreateRibbonGroup();
            this.btnNewInvoice = this.Factory.CreateRibbonButton();
            this.BtnSave = this.Factory.CreateRibbonButton();
            this.btnVerify = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.BtnDetails = this.Factory.CreateRibbonButton();
            this.BtnSkonto = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.BtnLoadTemplate = this.Factory.CreateRibbonButton();
            this.BtnSaveAsTemplate = this.Factory.CreateRibbonButton();
            this.btnChangeFormType = this.Factory.CreateRibbonButton();
            this.rbGrpSignSend = this.Factory.CreateRibbonGroup();
            this.btnSignAndMail = this.Factory.CreateRibbonButton();
            this.btnSendByMail = this.Factory.CreateRibbonButton();
            this.btnSendByService = this.Factory.CreateRibbonButton();
            this.rGrpServices = this.Factory.CreateRibbonGroup();
            this.btnUIDBestaetigung = this.Factory.CreateRibbonButton();
            this.btnVerifySignature = this.Factory.CreateRibbonButton();
            this.group6 = this.Factory.CreateRibbonGroup();
            this.BtnEditSettings = this.Factory.CreateRibbonButton();
            this.gallery1 = this.Factory.CreateRibbonGallery();
            this.gbtnKonto = this.Factory.CreateRibbonButton();
            this.gbtnHandySignatur = this.Factory.CreateRibbonButton();
            this.gbtnMail = this.Factory.CreateRibbonButton();
            this.gbtnUidAbfrage = this.Factory.CreateRibbonButton();
            this.gbtnSaveLoc = this.Factory.CreateRibbonButton();
            this.gbtnZustellung = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.BtnEbInterface = this.Factory.CreateRibbonButton();
            this.BtnAustriaPro = this.Factory.CreateRibbonButton();
            this.BtnSignatur = this.Factory.CreateRibbonButton();
            this.btnErbGvAt = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.BtnHelp = this.Factory.CreateRibbonButton();
            this.btnSupport = this.Factory.CreateRibbonButton();
            this.BtnAbout = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group7.SuspendLayout();
            this.group2.SuspendLayout();
            this.group1.SuspendLayout();
            this.rbGrpSignSend.SuspendLayout();
            this.rGrpServices.SuspendLayout();
            this.group6.SuspendLayout();
            this.group4.SuspendLayout();
            this.group3.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group5);
            this.tab1.Groups.Add(this.group7);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.rbGrpSignSend);
            this.tab1.Groups.Add(this.rGrpServices);
            this.tab1.Groups.Add(this.group6);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Label = "AUSTRIAPRO";
            this.tab1.Name = "tab1";
            // 
            // group5
            // 
            this.group5.Label = "group5";
            this.group5.Name = "group5";
            this.group5.Visible = false;
            // 
            // group7
            // 
            this.group7.Items.Add(this.btnNewInvoice);
            this.group7.Items.Add(this.BtnSave);
            this.group7.Items.Add(this.btnVerify);
            this.group7.Label = "eRechnung ";
            this.group7.Name = "group7";
            // 
            // btnNewInvoice
            // 
            this.btnNewInvoice.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnNewInvoice.Image = global::eRechnung.Images.NewDocument_32x32;
            this.btnNewInvoice.Label = "Neu";
            this.btnNewInvoice.Name = "btnNewInvoice";
            this.btnNewInvoice.ScreenTip = "Daten löschen";
            this.btnNewInvoice.ShowImage = true;
            this.btnNewInvoice.SuperTip = "Löscht alle Daten im Formular und setzt die Werte aus den Einstellungen ein.";
            // 
            // BtnSave
            // 
            this.BtnSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnSave.Image = global::eRechnung.Images.ebInterfaceSpeichern;
            this.BtnSave.Label = "eRechnung speichern";
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.ScreenTip = "Speichern";
            this.BtnSave.ShowImage = true;
            this.BtnSave.SuperTip = "Überprüft die Einträge im Formular gemäß ebInterface Standard und speichert die e" +
    "Rechnung in der ausgewählten Datei";
            // 
            // btnVerify
            // 
            this.btnVerify.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnVerify.Image = global::eRechnung.Images.SubmitForApproval_32x32;
            this.btnVerify.Label = "eRechnung prüfen";
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.ScreenTip = "Prüft die eRechnung";
            this.btnVerify.ShowImage = true;
            this.btnVerify.SuperTip = "Die aktuelle eRechnung wird gemäß ebInterface Standard und bei Bedarf gemäß eRech" +
    "nung an die öffentl. Verwaltung  geprüft.";
            // 
            // group2
            // 
            this.group2.Items.Add(this.BtnDetails);
            this.group2.Items.Add(this.BtnSkonto);
            this.group2.Label = "Bearbeiten";
            this.group2.Name = "group2";
            // 
            // BtnDetails
            // 
            this.BtnDetails.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnDetails.Image = global::eRechnung.Images.DetailsBearbeiten;
            this.BtnDetails.Label = "Positionen bearbeiten";
            this.BtnDetails.Name = "BtnDetails";
            this.BtnDetails.ScreenTip = "Rechnungspositionen bearbeiten";
            this.BtnDetails.ShowImage = true;
            this.BtnDetails.SuperTip = "Öffnet ein Fenster zur Bearbeitung der Einzelpositionen dieser Rechnung";
            // 
            // BtnSkonto
            // 
            this.BtnSkonto.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnSkonto.Image = global::eRechnung.Images.SkontoBearbeiten;
            this.BtnSkonto.Label = "Skonti bearbeiten";
            this.BtnSkonto.Name = "BtnSkonto";
            this.BtnSkonto.ScreenTip = "Skonti bearbeiten";
            this.BtnSkonto.ShowImage = true;
            this.BtnSkonto.SuperTip = "Öffnet ein Fenster zur Bearbeitung der Skonto Tabelle";
            // 
            // group1
            // 
            this.group1.Items.Add(this.BtnLoadTemplate);
            this.group1.Items.Add(this.BtnSaveAsTemplate);
            this.group1.Items.Add(this.btnChangeFormType);
            this.group1.Label = "Vorlagen";
            this.group1.Name = "group1";
            // 
            // BtnLoadTemplate
            // 
            this.BtnLoadTemplate.Image = global::eRechnung.Images.OpenSelectedItemHS;
            this.BtnLoadTemplate.Label = "Vorlage laden";
            this.BtnLoadTemplate.Name = "BtnLoadTemplate";
            this.BtnLoadTemplate.ScreenTip = "Vorlage laden";
            this.BtnLoadTemplate.ShowImage = true;
            this.BtnLoadTemplate.SuperTip = "Lädt eine eRechnungs-Vorlage aus der ausgewählten Datei";
            // 
            // BtnSaveAsTemplate
            // 
            this.BtnSaveAsTemplate.Image = global::eRechnung.Images.SaveFormDesignHS;
            this.BtnSaveAsTemplate.Label = "Vorlage speichern";
            this.BtnSaveAsTemplate.Name = "BtnSaveAsTemplate";
            this.BtnSaveAsTemplate.ScreenTip = "Vorlage speichern";
            this.BtnSaveAsTemplate.ShowImage = true;
            this.BtnSaveAsTemplate.SuperTip = "Speichert die Formular-Einträge als eRechnungs-Vorlage in der ausgwählten Datei";
            // 
            // btnChangeFormType
            // 
            this.btnChangeFormType.Image = global::eRechnung.Images.RefreshArrow_Blue_16x16_7;
            this.btnChangeFormType.Label = "Wechsel zu Wirtschaft";
            this.btnChangeFormType.Name = "btnChangeFormType";
            this.btnChangeFormType.ShowImage = true;
            this.btnChangeFormType.SuperTip = "Wechseln zwischen den Formularen für öffentl. Verwaltung und Wirtschaft";
            this.btnChangeFormType.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnToggleBund_Click);
            // 
            // rbGrpSignSend
            // 
            this.rbGrpSignSend.Items.Add(this.btnSignAndMail);
            this.rbGrpSignSend.Items.Add(this.btnSendByMail);
            this.rbGrpSignSend.Items.Add(this.btnSendByService);
            this.rbGrpSignSend.Label = "Versand";
            this.rbGrpSignSend.Name = "rbGrpSignSend";
            // 
            // btnSignAndMail
            // 
            this.btnSignAndMail.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSignAndMail.Image = global::eRechnung.Images.SignAndSendByEmail_32x32;
            this.btnSignAndMail.Label = "Signieren und per e-Mail versenden";
            this.btnSignAndMail.Name = "btnSignAndMail";
            this.btnSignAndMail.ScreenTip = "Signieren und versenden";
            this.btnSignAndMail.ShowImage = true;
            this.btnSignAndMail.SuperTip = "Überprüft die Rechnung gemäß ebInterface Standard, erstellt die A-Trust Handy Sig" +
    "natur für diese eRechnung und ermöglicht den Versand per E-Mail";
            // 
            // btnSendByMail
            // 
            this.btnSendByMail.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSendByMail.Image = global::eRechnung.Images.SendLinkByEmail_32x32;
            this.btnSendByMail.Label = "per e-Mail versenden";
            this.btnSendByMail.Name = "btnSendByMail";
            this.btnSendByMail.ScreenTip = "Per e-Mail versenden";
            this.btnSendByMail.ShowImage = true;
            this.btnSendByMail.SuperTip = "Prüft und speichert die aktuelle eRechnung als XMLK und PDF und öffnet ein Outloo" +
    "k Mail-Formular.";
            // 
            // btnSendByService
            // 
            this.btnSendByService.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSendByService.Image = global::eRechnung.Images.SendLinkByEmail_32x32;
            this.btnSendByService.Label = "Mit Zustelldienst versenden";
            this.btnSendByService.Name = "btnSendByService";
            this.btnSendByService.ScreenTip = "Versenden mit Zustelldienst";
            this.btnSendByService.ShowImage = true;
            this.btnSendByService.SuperTip = "Überprüft die Rechnung gemäß ebInterface Standard, erstellt ein PDF und ermöglich" +
    "t den Versand per separatem Zustelldienst-Programm";
            // 
            // rGrpServices
            // 
            this.rGrpServices.Items.Add(this.btnUIDBestaetigung);
            this.rGrpServices.Items.Add(this.btnVerifySignature);
            this.rGrpServices.Label = "Dienste";
            this.rGrpServices.Name = "rGrpServices";
            // 
            // btnUIDBestaetigung
            // 
            this.btnUIDBestaetigung.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUIDBestaetigung.Image = global::eRechnung.Images.FinanzOnline2;
            this.btnUIDBestaetigung.Label = "UID Bestätigung";
            this.btnUIDBestaetigung.Name = "btnUIDBestaetigung";
            this.btnUIDBestaetigung.ScreenTip = "UID Bestätigung über FinanzOnline für die UID des Rechnungsempfängers";
            this.btnUIDBestaetigung.ShowImage = true;
            this.btnUIDBestaetigung.SuperTip = "Sendet die UID des Rechnungsempfängers zur Überprüfung zum FinanzOnline des BMF";
            // 
            // btnVerifySignature
            // 
            this.btnVerifySignature.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnVerifySignature.Image = global::eRechnung.Images.PageVerifySignature;
            this.btnVerifySignature.Label = "Signatur prüfen";
            this.btnVerifySignature.Name = "btnVerifySignature";
            this.btnVerifySignature.ScreenTip = "Signatur prüfen";
            this.btnVerifySignature.ShowImage = true;
            this.btnVerifySignature.SuperTip = "Öffnet für die Signaturprüfung die Webseite der RTR";
            // 
            // group6
            // 
            this.group6.Items.Add(this.BtnEditSettings);
            this.group6.Items.Add(this.gallery1);
            this.group6.Label = "Einstellungen";
            this.group6.Name = "group6";
            // 
            // BtnEditSettings
            // 
            this.BtnEditSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnEditSettings.Image = global::eRechnung.Images.InvoicePreference;
            this.BtnEditSettings.Label = "Rechnungs- steller";
            this.BtnEditSettings.Name = "BtnEditSettings";
            this.BtnEditSettings.ScreenTip = "Einstellungen Rechnungssteller";
            this.BtnEditSettings.ShowImage = true;
            this.BtnEditSettings.SuperTip = "Öffnet ein Fenster zur Bearbeitung der Einstellungen zum Rechnungssteller";
            // 
            // gallery1
            // 
            this.gallery1.Buttons.Add(this.gbtnKonto);
            this.gallery1.Buttons.Add(this.gbtnHandySignatur);
            this.gallery1.Buttons.Add(this.gbtnMail);
            this.gallery1.Buttons.Add(this.gbtnUidAbfrage);
            this.gallery1.Buttons.Add(this.gbtnSaveLoc);
            this.gallery1.Buttons.Add(this.gbtnZustellung);
            this.gallery1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.gallery1.Image = global::eRechnung.Images.InvoicePreference;
            this.gallery1.Label = "Weitere Einstellungen";
            this.gallery1.Name = "gallery1";
            this.gallery1.ScreenTip = "Auswahl für weitere Einstellungen";
            this.gallery1.ShowImage = true;
            this.gallery1.SuperTip = "Öffnet eine Auswahlliste weiterer Einstellungen";
            // 
            // gbtnKonto
            // 
            this.gbtnKonto.Label = "Konto";
            this.gbtnKonto.Name = "gbtnKonto";
            this.gbtnKonto.ScreenTip = "Einstellungen für Konto bearbeiten";
            this.gbtnKonto.Visible = false;
            // 
            // gbtnHandySignatur
            // 
            this.gbtnHandySignatur.Label = "Handy-Signatur";
            this.gbtnHandySignatur.Name = "gbtnHandySignatur";
            this.gbtnHandySignatur.ScreenTip = "Einstellungen zur Handy-Signatur";
            this.gbtnHandySignatur.SuperTip = "Hier können die Einstellungen zur Handy-Signatur bearbeitet werden";
            this.gbtnHandySignatur.Visible = false;
            // 
            // gbtnMail
            // 
            this.gbtnMail.Label = "e-Mail";
            this.gbtnMail.Name = "gbtnMail";
            this.gbtnMail.ScreenTip = "Mail Einstellungen";
            this.gbtnMail.SuperTip = "Öffnet eine Fenster zum Bearbeiten der Mail Einstellungen";
            // 
            // gbtnUidAbfrage
            // 
            this.gbtnUidAbfrage.Label = "UID-Abfrage";
            this.gbtnUidAbfrage.Name = "gbtnUidAbfrage";
            this.gbtnUidAbfrage.ScreenTip = "Einstellungen für die UID Abfrage";
            this.gbtnUidAbfrage.SuperTip = "Öffnet ein Fenster zum Bearbeiten der Einstellungen für die UID Abfrage";
            // 
            // gbtnSaveLoc
            // 
            this.gbtnSaveLoc.Label = "Speicherorte";
            this.gbtnSaveLoc.Name = "gbtnSaveLoc";
            this.gbtnSaveLoc.ScreenTip = "Einstellungen für die Speicherorte";
            this.gbtnSaveLoc.SuperTip = "Öffnet ein Fenster zum Bearbeiten der Speicherorte";
            // 
            // gbtnZustellung
            // 
            this.gbtnZustellung.Label = "Zustelldienst";
            this.gbtnZustellung.Name = "gbtnZustellung";
            this.gbtnZustellung.ScreenTip = "Einstellungen für den Zustelldienst";
            this.gbtnZustellung.SuperTip = "Öffnet ein Fenster zum Bearbeiten der Einstellungen für den Zustelldienst";
            // 
            // group4
            // 
            this.group4.Items.Add(this.BtnEbInterface);
            this.group4.Items.Add(this.BtnAustriaPro);
            this.group4.Items.Add(this.BtnSignatur);
            this.group4.Items.Add(this.btnErbGvAt);
            this.group4.Label = "Informationen";
            this.group4.Name = "group4";
            // 
            // BtnEbInterface
            // 
            this.BtnEbInterface.Image = global::eRechnung.Images.WebInsertHyperlinkHS;
            this.BtnEbInterface.Label = "ebInterface";
            this.BtnEbInterface.Name = "BtnEbInterface";
            this.BtnEbInterface.ScreenTip = "ebInterface";
            this.BtnEbInterface.ShowImage = true;
            this.BtnEbInterface.SuperTip = "Öffenet die ebInterface Home Page";
            // 
            // BtnAustriaPro
            // 
            this.BtnAustriaPro.Image = global::eRechnung.Images.WebInsertHyperlinkHS;
            this.BtnAustriaPro.Label = "AUSTRIAPRO";
            this.BtnAustriaPro.Name = "BtnAustriaPro";
            this.BtnAustriaPro.ScreenTip = "AUSTRIAPRO";
            this.BtnAustriaPro.ShowImage = true;
            this.BtnAustriaPro.SuperTip = "Öffnet die Home Page der AUSTRIAPRO";
            // 
            // BtnSignatur
            // 
            this.BtnSignatur.Image = global::eRechnung.Images.WebInsertHyperlinkHS;
            this.BtnSignatur.Label = "Signatur";
            this.BtnSignatur.Name = "BtnSignatur";
            this.BtnSignatur.ScreenTip = "Signatur";
            this.BtnSignatur.ShowImage = true;
            this.BtnSignatur.SuperTip = "Öffnet eine Webseite der WKO mit Hinweisen zur digitalen Signatur";
            // 
            // btnErbGvAt
            // 
            this.btnErbGvAt.Image = global::eRechnung.Images.WebInsertHyperlinkHS;
            this.btnErbGvAt.Label = "ERECHNUNG.GV.AT";
            this.btnErbGvAt.Name = "btnErbGvAt";
            this.btnErbGvAt.ScreenTip = "Link zu ERECHNUNG.GV.AT";
            this.btnErbGvAt.ShowImage = true;
            this.btnErbGvAt.SuperTip = "Öffnet eine Webseite des BMF mit Informationen zur eRechnung an die öffentliche V" +
    "erwaltung";
            // 
            // group3
            // 
            this.group3.Items.Add(this.BtnHelp);
            this.group3.Items.Add(this.btnSupport);
            this.group3.Items.Add(this.BtnAbout);
            this.group3.Label = "Hilfe & Support";
            this.group3.Name = "group3";
            // 
            // BtnHelp
            // 
            this.BtnHelp.Image = global::eRechnung.Images.HilfeIcon;
            this.BtnHelp.Label = " Ausfüllhilfe";
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.ScreenTip = "Ausfüllhilfe";
            this.BtnHelp.ShowImage = true;
            // 
            // btnSupport
            // 
            this.btnSupport.Image = global::eRechnung.Images.pending_request_16x16_72;
            this.btnSupport.Label = "Support";
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.ScreenTip = "Link zum Supportforum";
            this.btnSupport.ShowImage = true;
            this.btnSupport.SuperTip = "Öffnet die Webseite zum ebInterface Forum";
            // 
            // BtnAbout
            // 
            this.BtnAbout.Image = global::eRechnung.Images.command_link_16x16;
            this.BtnAbout.Label = "Über das PlugIn";
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.ScreenTip = "Über das PlugIn";
            this.BtnAbout.ShowImage = true;
            this.BtnAbout.SuperTip = "Öffnet ein Fenster mit Angaben über das PlugIn";
            // 
            // EbI4P1Ribbon
            // 
            this.Name = "EbI4P1Ribbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ebI4p1Ribbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group7.ResumeLayout(false);
            this.group7.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.rbGrpSignSend.ResumeLayout(false);
            this.rbGrpSignSend.PerformLayout();
            this.rGrpServices.ResumeLayout(false);
            this.rGrpServices.PerformLayout();
            this.group6.ResumeLayout(false);
            this.group6.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNewInvoice;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnDetails;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnSkonto;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUIDBestaetigung;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rbGrpSignSend;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSignAndMail;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSendByService;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnVerifySignature;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnSaveAsTemplate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnLoadTemplate;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group6;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnEditSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnEbInterface;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnAustriaPro;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnSignatur;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnErbGvAt;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnHelp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSupport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnAbout;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup rGrpServices;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group7;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnVerify;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery gallery1;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnKonto;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnHandySignatur;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnMail;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnUidAbfrage;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnSaveLoc;
        private Microsoft.Office.Tools.Ribbon.RibbonButton gbtnZustellung;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSendByMail;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnChangeFormType;
    }

    partial class ThisRibbonCollection
    {
        internal EbI4P1Ribbon ebI4p1Ribbon
        {
            get { return this.GetRibbon<EbI4P1Ribbon>(); }
        }
    }
}
