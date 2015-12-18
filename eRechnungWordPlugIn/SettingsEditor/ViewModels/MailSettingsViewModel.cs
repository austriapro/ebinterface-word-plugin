using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ebIServices.SendMail;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using SettingsManager;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace SettingsEditor.ViewModels
{
    public class MailSettingsViewModel : ViewModelBase
    {
        private readonly ISendMailService _mailService;
        public MailSettingsViewModel(IDialogService dlg, ISendMailService mailService)
            : base(dlg)
        {
            _mailService = mailService;
            _subject = string.IsNullOrEmpty(PlugInSettings.Default.MailBetreff)
                ? PlugInSettings.Default.DefaultMailSubject
                : PlugInSettings.Default.MailBetreff;
            _body = string.IsNullOrEmpty(PlugInSettings.Default.MailText)
                ? PlugInSettings.Default.DefaultMailBody
                : PlugInSettings.Default.MailText;
        }
        private string _subject;
        [StringLengthValidator(1, 255, MessageTemplate = "MT00082 Der Betreff darf nicht leer sein.", Tag = "Betreff")]
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (_subject == value)
                    return;
                _subject = value;
                OnPropertyChanged();
            }
        }

        private string _body;
        [StringLengthValidator(1, 1024, MessageTemplate = "MT00083 Der Betreff darf nicht leer und max. {5} Zeichen lang sein.", Tag = "Betreff")]
        public string Body
        {
            get { return _body; }
            set
            {
                if (_body == value)
                    return;
                _body = value;
                OnPropertyChanged();
            }
        }

        public ValidationResults Results { get; private set; }

        #region Speichern Command

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => saveClick());
                return _saveCommand;
            }
        }

        private void saveClick()
        {
            if (!IsValid()) return;
            PlugInSettings.Default.MailBetreff = Subject;
            PlugInSettings.Default.MailText = Body;
            PlugInSettings.Default.Save();
        }
        #endregion
        #region Testen

        private ICommand _testenCommand;
        public ICommand TestenCommand
        {
            get
            {
                _testenCommand = _testenCommand ?? new RelayCommand(TestenClick);
                return _testenCommand;
            }
        }

        private void TestenClick(object obj)
        {
            if (!IsValid()) return;
            _mailService.Subject = SharedMethods.ReplaceToken(Subject, "123456", DateTime.Today,
                "Mail Test Firma",
                "Mail Test Kontaktname", "+43 (699) 123 123 456", "rechnung@testmail.com"
                );
            _mailService.MailBody = SharedMethods.ReplaceToken(Body, "123456", DateTime.Today,
                "Mail Test Firma",
                "Mail Test Kontaktname", "+43 (699) 123 123 456", "rechnung@testmail.com"
                );
            _mailService.SendMail();
        }
        #endregion

        public bool IsValid()
        {
            Validator<MailSettingsViewModel> validator = ValidationFactory.CreateValidator<MailSettingsViewModel>();
            Results = validator.Validate(this);
            return Results.IsValid;
        }

    }
}
