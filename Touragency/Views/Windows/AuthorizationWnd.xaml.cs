using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Touragency.Model;
using Touragency.ViewModels;

namespace Touragency.Views.Windows
{
    public partial class AuthorizationWnd : Window
    {
        private readonly AuthorizationViewModel _viewModel;
        private bool isPasswordShown = false; // Renamed for consistency
        private string password = string.Empty;

        public AuthorizationWnd()
        {
            InitializeComponent();
            _viewModel = new AuthorizationViewModel();
            _viewModel.AuthenticationFailed += OnAuthenticationFailed;
            _viewModel.AuthenticationSucceeded += OnAuthenticationSucceeded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = Tbx_Login.Text;
            string password = Tbx_Password.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Вы не ввели имя пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Вы не ввели пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _viewModel.Authenticate(login, password);
        }

        private void OnAuthenticationSucceeded(object sender, EventArgs e)
        {
            ShowMainScreenWnd();
        }

        private void OnAuthenticationFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowMainScreenWnd()
        {
            var mainScreen = new MainscreenWnd();
            mainScreen.Closed += MainWnd_Closed;
            mainScreen.Show();
            Hide();
        }

        private void MainWnd_Closed(object sender, EventArgs e)
        {
            if ((sender as MainscreenWnd)?.ChangingAccountStatus == true)
            {
                Show();
                return;
            }
            Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Pbx_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password == Pbx_Password.Password)
            {
                return;
            }
            password = Pbx_Password.Password;
            Tbx_Password.Text = password;
        }

        private void Tbx_Password_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (password == Tbx_Password.Text)
            {
                return;
            }
            password = Tbx_Password.Text;
            Pbx_Password.Password = password;
        }
        private void eyeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isPasswordShown)
            {
                Pbx_Password.Visibility = Visibility.Visible;
                Tbx_Password.Visibility = Visibility.Collapsed;
                isPasswordShown = false;
            }
            else
            {
                Pbx_Password.Visibility = Visibility.Collapsed;
                Tbx_Password.Visibility = Visibility.Visible;
                isPasswordShown = true;
            }
        }
    }
}