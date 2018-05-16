using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ContactMgmt.View;
using ContactMgmtCommon;

namespace ContactMgmt
{
    /// <summary>
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _contacts;

        public MainWindowViewModel()
        {
            GetAllContacts();
        }

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
        public event EventHandler CloseWindow;

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

        #region ICommand implementation

        public ICommand AddCommand => new DelegateCommand(AddContactCommand, null);
        private void AddContactCommand(object sender)
        {
            var contactForm = new ContactForm();
            ContactViewModel dataContext = ((ContactViewModel)contactForm.DataContext);
            dataContext.ContactInfo = new Contact();
            contactForm.ShowDialog();
            GetAllContacts();
        }
        public ICommand UpdateCommand => new DelegateCommand(UpdateContactCommand, canUpdateButtonEnable);

        private bool canUpdateButtonEnable(object obj)
        {

            return true; ;
        }

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
        public ICommand DeleteCommand => new DelegateCommand(DeleteContactCommand, canDeleteButtonEnable);

        private bool canDeleteButtonEnable(object obj)
        {
            return true;
        }

        private void DeleteContactCommand(object sender)
        {
            var deletContacts = (from contact in Contacts where contact.IsSelected select contact).ToList();

            using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
            {
                contactMgmt.DeleteContact(ref deletContacts);
            }

            GetAllContacts();
        }
        public ICommand ExitCommand => new DelegateCommand(Exit_Command, null);
        private void Exit_Command(object sender)
        {
            if (CloseWindow != null) CloseWindow(sender, null);
        }

        #endregion
    }
}