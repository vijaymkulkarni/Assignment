using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ContactMgmt.View;
using ContactMgmtCommon;

namespace ContactMgmt
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ContactsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Contact> _contacts;

        public ContactsViewModel()
        {
            using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
                _contacts = new ObservableCollection<Contact>(contactMgmt.GetAllContacts());
        }

        public ObservableCollection<Contact> Contacts => _contacts;

        public bool IsUpdateButtonEnable
        {
            get
            {
                var selectedContacts = (from contact in Contacts where contact.IsSelected select contact).ToList();
                RaisePropertyChanged("IsUpdateButtonEnable");
                return selectedContacts.Count() == 1;
            }
            set { }

        }

        public bool IsDeleteButtonEnable
        {
            get
            {
                var selectedContacts = (from contact in Contacts where contact.IsSelected select contact).ToList();
                RaisePropertyChanged("IsDeleteButtonEnable");
                return selectedContacts.Any();
            }
            set { }
        }

        #region ICommand implementation

        public ICommand AddCommand => new DelegateCommand(AddContactCommand);

        private void AddContactCommand(object sender)
        {
            MessageBox.Show("Hello");
            //ContactForm contactForm = new ContactForm();
            //contactForm.ShowDialog();            
        }

        public ICommand UpdateCommand => new DelegateCommand(UpdateContactCommand);

        private void UpdateContactCommand(object sender)
        {
            List<Contact> selectedContacts = (from c in _contacts where c.IsSelected select c).ToList();
            if (selectedContacts.Count == 1)
            {
                var contactForm = new ContactForm();
                var contactViewModel = ((ContactViewModel) contactForm.DataContext);
                contactViewModel.FirstName = selectedContacts[0].FirstName;
                contactViewModel.LastName = selectedContacts[0].LastName;
                contactViewModel.EmailAddress = selectedContacts[0].EmailAddress;
                contactViewModel.PhoneNumber = selectedContacts[0].PhoneNumber;
                contactViewModel.Status = selectedContacts[0].Status;
                contactForm.ShowDialog();
            }
        }
        
        public ICommand ExitCommand => new DelegateCommand(Exit_Command);

        internal void Exit_Command(object sender)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }

        public ICommand DeleteCommand => new DelegateCommand(DeleteContactCommand);

        private void DeleteContactCommand(object sender)
        {
            List<Contact> deletContacts = (from contact in Contacts where contact.IsSelected select contact).ToList();

            using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
            {
                contactMgmt.DeleteContact(deletContacts);

                _contacts = new ObservableCollection<Contact>(contactMgmt.GetAllContacts());
            }

        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        
    }    
}
