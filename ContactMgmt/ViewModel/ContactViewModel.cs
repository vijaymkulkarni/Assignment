using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ContactMgmtCommon;

namespace ContactMgmt
{
    public class ContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CloseWindow;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region Properties

        /// <summary>
        /// contact status binding values to status drop down.
        /// </summary>
        public class ContactStatus
        {
            public ContactStatus(string name)
            {
                StatusName = name;
            }

            public string StatusName { get; set; }
        }

        private List<ContactStatus> _contactStatuses;

        /// <summary>
        /// Contact Status Binding property to status combobox
        /// </summary>
        public List<ContactStatus> ContactStatuses
        {
            get
            {
                IList<ContactStatus> list = new List<ContactStatus>
                {
                    new ContactStatus("Active"),
                    new ContactStatus("In-Active")
                };
                _contactStatuses = new List<ContactStatus>(list);
                return _contactStatuses;
            }
            set { _contactStatuses = value; }
        }

        private Contact _contactInfo;
        /// <summary>
        /// Property to hold contact information
        /// </summary>
        public Contact ContactInfo
        {
            get
            { return _contactInfo == null ? _contactInfo = new Contact() : _contactInfo; }
            set
            {
                _contactInfo = value;
                RaisePropertyChanged("ContactInfo");
            }
        }

        /// <summary>
        /// Contact Id : Primary Key
        /// </summary>
        public long ContactId
        {
            get
            { return ContactInfo.ContactId; }
            set
            {
                ContactInfo.ContactId = value;
                RaisePropertyChanged("ContactId");
            }
        }

        /// <summary>
        /// First Name of Contact
        /// </summary>
        public string FirstName
        {
            get
            { return ContactInfo.FirstName; }
            set
            {
                ContactInfo.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Last Name of contact
        /// </summary>
        public string LastName
        {
            get
            { return ContactInfo.LastName; }
            set
            {
                ContactInfo.LastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Email address of contact
        /// </summary>
        public string EmailAddress
        {
            get
            { return ContactInfo.EmailAddress; }
            set
            {
                ContactInfo.EmailAddress = value;
                RaisePropertyChanged("EmailAddress");
            }
        }

        /// <summary>
        /// Phone Number of contact Defined format (+XX XXX XXXXXXXXX) / (+XX XXXXXXXXXXX)
        /// </summary>
        public string PhoneNumber
        {
            get
            { return ContactInfo.PhoneNumber; }
            set
            {
                ContactInfo.PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        /// <summary>
        ///     Status (Possible values: Active/Inactive)
        /// </summary>
        public string Status
        {
            get
            { return ContactInfo.Status; }
            set
            {
                ContactInfo.Status = value;
                RaisePropertyChanged("Status");
            }
        }

        #endregion

        #region Command Implmentation

        /// <summary>
        /// Save button command binding event.
        /// </summary>
        public ICommand SaveCommand => new DelegateCommand(Save_Command, null);
        
        private void Save_Command(object sender)
        {
            var errorMsg = _contactInfo.Validate();
            if (string.IsNullOrEmpty(errorMsg))
            {
                using (IContactService contactMgmt = new ContactMgmtService.ContactMgmtService())
                {
                    if (_contactInfo.ContactId > 0)
                        contactMgmt.UpdateContact(ref _contactInfo);
                    else
                        contactMgmt.InsertContact(ref _contactInfo);
                }

                if (CloseWindow != null) CloseWindow(sender, null);
            }
            else
            {
                MessageBox.Show(errorMsg, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// cancel / Exit button command binding event
        /// </summary>
        public ICommand CancelCommand => new DelegateCommand(Cancel_Command, null);

        private void Cancel_Command(object sender)
        {
            if (CloseWindow != null)
            {
                LogHelper.Log(LogType.Information, "Contact Window successful exit");
                CloseWindow(sender, null);
            }
            else
                LogHelper.Log(LogType.Information, "Close window handle is missing");
        }

        #endregion
    }
}