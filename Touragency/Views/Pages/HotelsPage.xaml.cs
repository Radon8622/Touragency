using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Touragency.Model;
using Touragency.ViewModels;
using Touragency.Views.UsersControls;
using Touragency.Views.Windows;

namespace Touragency.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        public ObservableCollection<HotelCard> HotelsCards
        {
            get
            {
                return _viewModel.HotelCards;
            }
            set
            {
                _viewModel.HotelCards = value;
            }
        }

        private readonly HotelsViewModel _viewModel;

        public HotelPage()
        {
            InitializeComponent();
            _viewModel = new HotelsViewModel();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.HotelCards))
                {
                    ShowTourCards();
                }
            };
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (HotelsBoxesWpl.Children.Count == 0)
            {
                loadingTbx.Visibility = Visibility.Visible;
            }
            if (!FiltersModeMit.IsChecked)
                searchFiltersCdf.Width = new GridLength(0);

            ShowTourCards();

            new Task(() => { _viewModel.LoadHotelCardsAsync(); }).Start();
        }

        private void CategoryCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            ShowTourCards();
        }

        private async void ShowTourCards()
        {
            if (_viewModel is null) return;

            var filteredCards = await _viewModel.GetFilteredHotelCardsAsync(
                searchTbx.Text.ToLowerInvariant(),
                lowStars_SortingModeRbn.IsChecked == true,
                hightStars_SortingModeRbn.IsChecked == true);

            HotelsBoxesWpl.Children.Clear();
            foreach (var hotelCard in filteredCards)
            {
                var cardBox = new HotelCardBox(hotelCard)
                {
                    PreviewImage = hotelCard.CachedImage // Use cached image
                };
                HotelsBoxesWpl.Children.Add(cardBox);
            }
        }

        private async void searchTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(300);
            ShowTourCards();
        }

        private void SortingModeRbn_Checked(object sender, RoutedEventArgs e) => ShowTourCards();
        private void ActualityModeRbn_Checked(object sender, RoutedEventArgs e) => ShowTourCards();

        private void FiltersModeMit_Checked(object sender, RoutedEventArgs e)
        {
            searchFiltersCdf.Width = new GridLength(255);
        }

        private void FiltersModeMit_Unchecked(object sender, RoutedEventArgs e)
        {
            searchFiltersCdf.Width = new GridLength(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FiltersModeMit != null)
                FiltersModeMit.IsChecked = !FiltersModeMit.IsChecked;
        }

        private void HotelsBoxesWpl_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (HotelsBoxesWpl.Children.Count > 0)
            {
                loadingTbx.Visibility = Visibility.Collapsed;
                searchNotFoundResultsTbx.Visibility = Visibility.Collapsed;
                return;
            }
            else if (HotelsBoxesWpl.Children.Count == 0 && !string.IsNullOrWhiteSpace(searchTbx.Text))
            {
                loadingTbx.Visibility = Visibility.Collapsed;
                searchNotFoundResultsTbx.Visibility = Visibility.Visible;
                return;
            }
            loadingTbx.Visibility = Visibility.Visible;
        }
    }
}
