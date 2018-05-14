using System;
using System.Windows;

namespace ContactMgmt
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }

        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}