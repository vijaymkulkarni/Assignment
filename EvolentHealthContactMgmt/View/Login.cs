using System;
using System.ServiceModel;
using System.Windows;
using ContactMgmtCommon;
using ContactMgmtService;

namespace ContactMgmt
{
    /// <summary>
    ///     Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LogOn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ILoginService contactMgmt = new AppLoginManager();
                var returnValue = contactMgmt.ValidateLogin(new LoginInfo(TLoginUserName.Text, TPasswordBox.Password));
                if (!returnValue)
                {
                    MessageBox.Show("Invalid login name / password, please try again.", "Contact Manager Sign In",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var mainWindow = new MainWindow();
                    Close();
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
    }
}