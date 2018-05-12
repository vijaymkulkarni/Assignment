using ContactMgmtCommon;
using System.ComponentModel;
using System.Windows.Input;

namespace ContactMgmt
{
    public class ContactViewModel : INotifyPropertyChanged
    {
        private Contact _contactInfo;

        /// <summary>
        /// 
        /// </summary>
        public ContactViewModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactInfo"></param>
        public void Contact(Contact contactInfo)
        {
            _contactInfo = contactInfo;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get => _contactInfo.FirstName;
            set => _contactInfo.FirstName = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LastName
        {
            get => _contactInfo.LastName;
            set => _contactInfo.LastName = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress
        {
            get => _contactInfo.EmailAddress;
            set => _contactInfo.EmailAddress = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber
        {
            get => _contactInfo.PhoneNumber;
            set => _contactInfo.PhoneNumber = value;
        }

        /// <summary>
        /// Status(Possible values: Active/Inactive)
        /// </summary>
        public bool Status
        {
            get => _contactInfo.Status;
            set => _contactInfo.Status = value;
        }

        public ICommand SaveCommand => new DelegateCommand(Save_Command);

        private void Save_Command(object sender)
        {

        }

        public ICommand CancelCommand => new DelegateCommand(Cancel_Command);

        private void Cancel_Command(object sender)
        {

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
