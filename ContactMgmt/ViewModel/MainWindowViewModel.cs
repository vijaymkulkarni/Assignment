using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Caching;
using System.Windows.Input;
using ContactMgmt.View;
using ContactMgmtCommon;

namespace ContactMgmt
{
    /// <summary>
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        const string CACHE_KEY = "LoginInfo";


        private bool _isButtonEnable = true;
        /// <summary>
        /// Login user name
        /// </summary>
        public bool IsButtonEnable
        {
            get
            { return _isButtonEnable; }
            set
            {
                _isButtonEnable = value;
                RaisePropertyChanged("IsButtonEnable");
            }
        }

        private ObservableCollection<Contact> _contacts;
        
        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindowViewModel()
        {
            LoginInfo login;
            ObjectCache cache = MemoryCache.Default;            
            login = cache.Get(CACHE_KEY) as LoginInfo;
            if (login != null)
            {
                IsButtonEnable = login.Loginrole.ToUpper().Trim() == "ADMIN";
            }
            GetAllContacts();
        }

        /// <summary>
        /// All contacts binding to Grid control
        /// </summary>
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return _contacts == null ? _contacts = new ObservableCollection<Contact>() : _contacts;
            }
            set
            {
                _contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event handler when user clicks on Exit / Close button.
        /// </summary>
        public event EventHandler CloseWindow;

        #region ICommand implementation
        
        /// <summary>
        /// Add contact button command 
        /// </summary>
        public ICommand AddCommand => new DelegateCommand(AddContactCommand, null);
        private void AddContactCommand(object sender)
        {
            var contactForm = new ContactForm();
            ContactViewModel dataContext = ((ContactViewModel)contactForm.DataContext);
            dataContext.ContactInfo = new Contact();
            contactForm.ShowDialog();
            GetAllContacts();
        }

        /// <summary>
        /// Update contact button command
        /// </summary>
        public ICommand UpdateCommand => new DelegateCommand(UpdateContactCommand, null);
        private void UpdateContactCommand(object sender)
        {
            var selectedContacts = (from c in Contacts where c.IsSelected select c).ToList();
            if (selectedContacts.Count == 1)
            {
                var contactForm = new ContactForm();
                var viewModel = (ContactViewModel) contactForm.DataContext;
                viewModel.ContactInfo = selectedContacts[0];
                viewModel.ContactId = selectedContacts[0].ContactId;
                viewModel.FirstName = selectedContacts[0].FirstName;
                viewModel.LastName = selectedContacts[0].LastName;
                viewModel.EmailAddress = selectedContacts[0].EmailAddress;
                viewModel.PhoneNumber = selectedContacts[0].PhoneNumber;
                viewModel.Status = selectedContacts[0].Status;
                contactForm.ShowDialog();
                GetAllContacts();
            }
        }

        /// <summary>
        /// Delete contact(s) button command
        /// </summary>
        public ICommand DeleteCommand => new DelegateCommand(DeleteContactCommand, null);
        private void DeleteContactCommand(object sender)
        {
            var deletContacts = (from contact in Contacts where contact.IsSelected select contact).ToList();

            using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
            {
                contactMgmt.DeleteContact(ref deletContacts);
            }

            GetAllContacts();
        }

        /// <summary>
        /// Exit/Close button command.
        /// </summary>
        public ICommand ExitCommand => new DelegateCommand(Exit_Command, null);
        private void Exit_Command(object sender)
        {
            if (CloseWindow != null) CloseWindow(sender, null);
        }

        #endregion

        #region Private Helper Routines

        private void GetAllContacts()
        {
            using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
            {
                Contacts = new ObservableCollection<Contact>(contactMgmt.GetAllContacts());
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion

    }
}