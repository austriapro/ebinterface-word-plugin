using System.Diagnostics;
using ebIModels.Models;
using ebIModels.Schema;
using ebIServices.SendMail;
using ebIServices.StartProcess;
using ebIServices.UidAbfrage;
using ebIViewModels.ErrorView;
using ebIViewModels.RibbonViewModels;
using ebIViewModels.RibbonViews;
using ebIViewModels.Services;
using ebIViewModels.ViewModels;
using ebIViewModels.Views;
using eRechnung.Views;
using EventBrokerExtension;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using SettingsEditor.Service;
using SettingsEditor.ViewModels;
using SettingsEditor.Views;
using SimpleEventBroker;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using ebISaveFileDialog; 

namespace eRechnung
{
    public partial class ThisDocument
    {
        public void RegisterEventSubscriber()
        {
            // Register SetDropDownSelection Event Handler
            EventBroker eb = UContainer.Resolve(typeof(EventBroker)) as EventBroker;
            eb.RegisterSubscriber(UpdatePropertyEventArgs.UpdateDropDownSelection, SetDropDownSelection);
            eb.RegisterSubscriber(UpdatePropertyEventArgs.UpdateProtectedProperty, UpdateProtectedProperty);
            eb.RegisterSubscriber(UpdatePropertyEventArgs.UpdateDocTable, UpdateDocTable);
            eb.RegisterSubscriber(InvoiceViewModel.InvoiceValidationOptionChanged, UpdateBookmarks);
            eb.RegisterSubscriber(UpdatePropertyEventArgs.ShowPanelEvent, ShowErrorPane);
            eb.RegisterSubscriber(InvoiceViewModel.SendMailEvent, OnSendMailEvent);
            eb.RegisterSubscriber(InvoiceViewModel.SaveAsPdfEvent, OnSaveAsPdfEvent);
            eb.RegisterSubscriber(InvoiceViewModel.DocumentHomeKey, OnDocumentHomeKey);

        }
        public void RegisterSingleEventSubscriber(string publishedEventName, System.EventHandler subscriber)
        {
            EventBroker eb = UContainer.Resolve(typeof(EventBroker)) as EventBroker;
            eb.RegisterSubscriber(publishedEventName, subscriber);
        }
        public static IUnityContainer Register4Unity()
        {
            IConfigurationSource source = ConfigurationSourceFactory.Create();
            var validationFactory = ConfigurationValidatorFactory.FromConfigurationSource(source);
            // Unitiy Container
         IUnityContainer   uc = new UnityContainer()
                .AddNewExtension<SimpleEventBrokerExtension>()
                .AddNewExtension<Interception>();

            uc.RegisterInstance(uc, new ContainerControlledLifetimeManager());

            uc.RegisterInstance<ValidatorFactory>(validationFactory);
         //   uc.RegisterInstance<ThisDocument>(Globals.ThisDocument);

            // Error Handler
            uc.RegisterType<ErrorViewModel>();
            uc.RegisterType<ErrorActionPaneViewModel>();
            uc.RegisterType<ErrorActionsPaneControl>();

            // RegisterEventSubscriber();

            // Dialogservice
            uc.RegisterType<ISaveFileDialog, SaveFileDialogViewModel>();
            uc.RegisterType<IOpenFileDialog, OpenFileDialogViewModel>();
            uc.RegisterType<IFolderBrowserDialog, FolderBrowserDialogViewModel>();
            uc.RegisterType<IDialogService, DialogService>();
            uc.RegisterType<FrmSelectVersion>();

            Invoice = InvoiceFactory.CreateInvoice();
            uc.RegisterInstance<IInvoiceModel>(Invoice, new ContainerControlledLifetimeManager());

            // Viewmodels

            uc.RegisterType<RibbonViewModel>();
            uc.RegisterType<InvoiceViewModel>();
            uc.RegisterType<VatViewModel>();
            uc.RegisterType<DetailsViewModel>();
            uc.RegisterType<DetailsViewModels>();
            uc.RegisterType<SkontoViewModel>();
            uc.RegisterType<SkontoViewModels>();
            uc.RegisterType<ProgressViewModel>();
            uc.RegisterType<AboutViewModel>();
            uc.RegisterType<RelatedDocumentType>();

            // Settings
            uc.RegisterType<SettingsViewModel>();

            uc.RegisterType<CurrencyListViewModel>();
            uc.RegisterType<CurrencyListViewModels>();
            uc.RegisterType<BillerSettingsViewModel>();
            uc.RegisterType<HandySignSettingsViewModel>();
            uc.RegisterType<KontoSettingsViewModel>();
            uc.RegisterType<MailSettingsViewModel>();
            uc.RegisterType<SaveLocationSettingsViewModel>();
            uc.RegisterType<SettingsViewModel>();
            uc.RegisterType<UidAbfrageSettingsViewModel>();
            uc.RegisterType<ZustellSettingsViewModel>();

            // Settings Forms
            uc.RegisterType<FrmBillerSettingsView>();
            uc.RegisterType<FrmHandySignSettingsView>();
            uc.RegisterType<FrmKontoSettingsView>();
            uc.RegisterType<FrmMailSettingsView>();
            uc.RegisterType<FrmSaveLocationView>();
            uc.RegisterType<FrmUidAbfrageSettingsView>();

            // Forms
            uc.RegisterType<FrmShowProgress>();
            uc.RegisterType<FrmDetailsList>();
            uc.RegisterType<FrmDetailsEdit>();
            uc.RegisterType<FrmSkontoList>();
            uc.RegisterType<FrmAboutView>();
            uc.RegisterType<FrmBillerSettingsView>();
 


            // Services
            uc.RegisterType<ISendMailService, SendMailService>();
            uc.RegisterType<IUidAbfrageDienst, UidAbfrageDienst>();
            uc.RegisterType<IStartProcessDienst, StartProcessDienst>();

            //foreach (var reg in uc.Registrations)
            //{
            //    Log.LogWrite(CallerInfo.Create(),"Name:" + reg.Name + ", mapped to:" + reg.MappedToType.Name);
            //}
            return uc;
        }
    }
}