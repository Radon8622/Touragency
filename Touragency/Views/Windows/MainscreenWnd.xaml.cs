using System.Windows;
using Touragency.Model;
using Touragency.ViewModels;
using Touragency.Views.Pages;

namespace Touragency.Views.Windows
{
    public partial class MainscreenWnd : Window
    {
        public bool ChangingAccountStatus { get; set; }
        private readonly MainScreenViewModel _viewModel;

        public MainscreenWnd()
        {
            InitializeComponent();
            _viewModel = new MainScreenViewModel(this);
            InitializeContent();
        }

        private void InitializeContent()
        {
            var toursPage = _viewModel.GetToursPage();
            var hotelsPage = _viewModel.GetHotelsPage();
            ToursViewFrame.Navigate(toursPage);
            HotelsViewFrame.Navigate(hotelsPage);
        }

        private void ChangeAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ConfirmLogout())
            {
                ChangingAccountStatus = true;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            greetingTbx.Text += $" {Session.User.Username}";
            if (Session.User.Groups.Count > 0)
            {
                greetingTbx.ToolTip = $"Вы вошли как {Session.User.Groups[0].Name.ToLower()}";
            }
            else
            {
                greetingTbx.ToolTip = string.Empty;
            }
            Session.MainscreenWnd = this;
        }

        public void OpenHotelPageAndInsertSearchText(string searchText)
        {
            _viewModel.OpenHotelPageAndInsertSearchText(searchText);
        }
    }
}