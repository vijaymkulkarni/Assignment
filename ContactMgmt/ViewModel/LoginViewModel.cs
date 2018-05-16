using ContactMgmtCommon;
using ContactMgmtService;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

namespace ContactMgmt
{
    public class LoginViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CloseWindow;

        public event EventHandler GetPasswordToViewModel;

        private string _LoginName;
        /// <summary>
        /// </summary>
        public string LoginName
        {
            get
            { return _LoginName; }
            set
            {
                _LoginName = value;
                RaisePropertyChanged("LoginName");
            }
        }

        private string _Password;
        /// <summary>
        /// </summary>
        public string Password
        {
            get
            { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }

        public ICommand AuthenticateCommand => new DelegateCommand(Authenticate_Command, canAuthenticate);

        private bool canAuthenticate(object obj)
        {
            if (string.IsNullOrEmpty(LoginName))
                return false;

            return true;
        }

        private void Authenticate_Command(object obj)
        {
            try
            {
                if (GetPasswordToViewModel != null) GetPasswordToViewModel(null, null);
                                
                ILoginService contactMgmt = new AppLoginManager();
                var returnValue = contactMgmt.ValidateLogin(new LoginInfo(LoginName, Password));
                if (!returnValue)
                {
                    MessageBox.Show("Invalid login name / password, please try again.", "Contact Manager Sign In",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var mainWindow = new MainWindow();
                    if (CloseWindow != null) CloseWindow(null, null);
                    mainWindow.ShowDialog();
                }
            }
            catch (FaultException<CustomException> ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception");
            }
        }
                
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                canAuthenticate(null);
            }
        }
    }
}
