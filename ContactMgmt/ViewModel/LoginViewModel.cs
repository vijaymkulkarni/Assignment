using ContactMgmtCommon;
using ContactMgmtService;
using System;
using System.ComponentModel;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

namespace ContactMgmt
{
    
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel
    {
        const string CACHE_KEY = "LoginInfo";

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Close Login Window binding
        /// </summary>
        public event EventHandler CloseWindow;

        /// <summary>
        /// Get Password from View.
        /// When you try to databind the password property of a PasswordBox you will recognize that you cannot do data binding on it. The reason for this is, that the password property is not backed by a DependencyProperty.
        /// Reference: https://www.wpftutorial.net/PasswordBox.html
        /// </summary>
        public event EventHandler GetPasswordToViewModel;

        private string _loginName;
        /// <summary>
        /// Login user name
        /// </summary>
        public string LoginName
        {
            get
            { return _loginName; }
            set
            {
                _loginName = value;
                RaisePropertyChanged("LoginName");
            }
        }

        private string _password;
        /// <summary>
        /// Password of Logged-in user
        /// </summary>
        public string Password
        {
            get
            { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// Authenticate Command to Login into system / application.
        /// </summary>
        public ICommand AuthenticateCommand => new DelegateCommand(Authenticate_Command, null);

        private void Authenticate_Command(object obj)
        {
            try
            {
                if (GetPasswordToViewModel != null) GetPasswordToViewModel(null, null);

                ILoginService contactMgmt = new AppLoginManager();
                LoginInfo login = new LoginInfo(LoginName, Password);
                var returnValue = contactMgmt.ValidateLogin(login);
                if (!returnValue)
                {
                    MessageBox.Show("Invalid login name / password, please try again.", "Contact Manager Sign In",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ObjectCache cache = MemoryCache.Default;
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1.0)
                    };
                    cache.Add(CACHE_KEY, login, cacheItemPolicy);

                    var mainWindow = new MainWindow();
                    if (CloseWindow != null) CloseWindow(null, null);
                    mainWindow.ShowDialog();
                }
            }
            catch (FaultException<CustomException> ex)
            {
                LogHelper.Log(LogType.Error, String.Concat(ex.Message, Environment.NewLine, ex.StackTrace));

                MessageBox.Show(ex.Message, "Exception");
            }
            catch (Exception exception)
            {
                LogHelper.Log(LogType.Error, String.Concat(exception.Message, Environment.NewLine, exception.StackTrace));
                MessageBox.Show(exception.Message, "Exception");
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
