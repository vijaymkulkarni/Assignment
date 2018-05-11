using System.ComponentModel;

/// <summary>
/// 
/// </summary>
namespace EvolentHealthContactMgmt
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactsViewModel : INotifyPropertyChanged
    {

        public ContactsViewModel()
        {
        }
        
        private string _firstName;
        /// <summary>
        /// 
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; RaisePropertyChanged("FirstName"); }
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

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
