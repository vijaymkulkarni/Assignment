using System;
using System.Windows;

namespace ContactMgmt.View
{
    /// <summary>
    ///     Interaction logic for ContactForm.xaml
    /// </summary>
    public partial class ContactForm : Window
    {
        public ContactForm()
        {
            InitializeComponent();
            ContactViewModel viewModel;
            if (DataContext == null)
            {
                viewModel = new ContactViewModel();
                DataContext = viewModel;
            }

            viewModel = (ContactViewModel) DataContext;
            viewModel.CloseWindow += ViewModel_CloseWindow;
            Loaded += ContactForm_Loaded;
        }

        private void ContactForm_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}