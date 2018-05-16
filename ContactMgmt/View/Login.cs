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
            var viewModel = new LoginViewModel();
            DataContext = viewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
            viewModel.GetPasswordToViewModel += GetPasswordToViewModel;
        }

        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            LogHelper.Log(LogType.Information, "Login Window successful exit");
            Close();
        }

        private void GetPasswordToViewModel(object sender, EventArgs e)
        {
            var viewModel = (LoginViewModel)DataContext;
            viewModel.Password = TPasswordBox.Password;
        }        
    }
}