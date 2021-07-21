using IT_Project.ViewModels;
using System.Windows;
using System.Windows.Controls;

//Leah Oswald 7/20/2021

namespace IT_Project.UserControls
{
    // Interaction logic for LoginView.xaml

    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();

            _viewModel = (LoginViewModel)this.Resources["viewModel"];
        }
        private LoginViewModel _viewModel = null;

        // Login button event handler for login view.
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CurrentUser.Password = pword.Password;
            _viewModel.Login();
        }

        // Cancel button event handler for login view.
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Close();
        }
    }
}
