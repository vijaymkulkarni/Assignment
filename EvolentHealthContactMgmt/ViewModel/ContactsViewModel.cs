using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ContactMgmtCommon;

namespace ContactMgmt
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ContactsViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<Contact> _contacts;

        public ContactsViewModel()
        {
            IContactService contactMgmt = new ContactMgmtService.ContactMgmtService();
            _contacts = new ObservableCollection<Contact>(contactMgmt.GetAllContacts());
        }

        public ObservableCollection<Contact> Contacts => _contacts;

        public ICommand AddCommand => new DelegateCommand(AddContactCommand);

        private void AddContactCommand(object sender)
        {
            //throw new NotImplementedException();
        }

        public ICommand UpdateCommand => new DelegateCommand(UpdateContactCommand);

        private void UpdateContactCommand(object sender)
        {
            //throw new NotImplementedException();
        }
        
        public ICommand ExitCommand => new DelegateCommand(Exit_Command);

        internal void Exit_Command(object sender)
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
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
            //throw new NotImplementedException();
        }

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
