using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class ToursPage : Page
    {
        public ObservableCollection<TourCard> TourCards { 
            get
            {
                return _viewModel.TourCards;
            }
            set
            {
                _viewModel.TourCards = value;
            }
        }

        private readonly ToursViewModel _viewModel;

        public ToursPage()
        {
            InitializeComponent();
            _viewModel = new ToursViewModel();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.TourCards))
                {
                    ShowTourCards();
                }
            };
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!FiltersModeMit.IsChecked)
                searchFiltersCdf.Width = new GridLength(0);

            await _viewModel.LoadTourCardsAsync();

            if (!Session.CheckTourUpdatePermission(Permissions.UpdateTour) && !Session.CheckTourUpdatePermission(Permissions.DeleteTour))
            {
                EditModeMit.Visibility = Visibility.Collapsed;
            }
            if (!Session.CheckTourUpdatePermission(Permissions.CreateTour))
            {
                CreateModeMit.Visibility = Visibility.Collapsed;
            }

            UpdateCategoiesFiltersUI();
            relevant_SortingModeRbn.IsChecked = true;
            ShowTourCards();
            UpdatePriceRangeUI();
        }

        private void UpdateCategoiesFiltersUI()
        {
            TourCardsManager.AllRegistratedTourCategoriesNames = _viewModel.GetDistinctCategories();

            categoriesSpl.Children.Clear();
            foreach (var category in TourCardsManager.AllRegistratedTourCategoriesNames)
            {
                var checkBox = new CheckBox
                {
                    Content = category,
                    Margin = new Thickness(5),
                    FontFamily = new FontFamily("Comic Sans MS")
                };

                checkBox.Checked += CategoryCheckBox_Changed;
                checkBox.Unchecked += CategoryCheckBox_Changed;
                categoriesSpl.Children.Add(checkBox);
            }
        }
        private void UpdatePriceRangeUI()
        {
            var allToursCard = _viewModel.TourCards.ToList();
            uint max = allToursCard.OrderByDescending(t => t.Price).First().Price;
            uint min = allToursCard.OrderBy(t => t.Price).First().Price;

            priceRangeSelector.MinPrice = (int)min;
            priceRangeSelector.MaxPrice = (int)max;
        }

        private void CategoryCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            ShowTourCards();
        }

        private async void ShowTourCards()
        {
            if (_viewModel is null) return;

            var selectedCategories = categoriesSpl.Children
                .OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => cb.Content.ToString())
                .ToList();

            var filteredCards = await _viewModel.GetFilteredTourCardsAsync(
                searchTbx.Text.ToLowerInvariant(),
                relevant_SortingModeRbn.IsChecked == true,
                cheaper_SortingModeRbn.IsChecked == true,
                actual_ActualityModeRbn.IsChecked == true,
                nonactual_ActualityModeRbn.IsChecked == true,
                (uint)priceRangeSelector.MinPrice,
                (uint)priceRangeSelector.MaxPrice,
                selectedCategories);

            TourBoxesSpl.Children.Clear();
            foreach (var tourCard in filteredCards)
            {
                var cardBox = new TourCardBox(tourCard)
                {
                    PreviewImage = tourCard.CachedImage // Use cached image
                };
                cardBox.TourDeleted += CardBox_TourDeleted;
                cardBox.TourEdited += CardBox_TourEdited;
                cardBox.TourRestored += CardBox_TourDeleted;
                cardBox.SearchHotelsButtonClick += CardBox_SearchHotelsButtonClick;

                if (EditModeMit.IsChecked == true)
                    cardBox.ShowEditControls();

                TourBoxesSpl.Children.Add(cardBox);
            }
        }

        private void CardBox_SearchHotelsButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is TourCardBox cardBox)
            {
                Session.MainscreenWnd.OpenHotelPageAndInsertSearchText(cardBox.TourName);
            }
        }

        private void CardBox_TourDeleted(object sender, RoutedEventArgs e)
        {
            if (sender is TourCardBox restoredCardBox)
            {
                _viewModel.UpdateTourCard(restoredCardBox);
                UpdateCategoiesFiltersUI();
                ShowTourCards();
            }
        }

        private void CardBox_TourEdited(object sender, RoutedEventArgs e)
        {
            if (sender is TourCardBox editedCardBox)
            {
                _viewModel.EditTourCard(editedCardBox);
                UpdateCategoiesFiltersUI();
                ShowTourCards();
                UpdatePriceRangeUI();
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

        private void EditModeChangeMit_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var card in TourBoxesSpl.Children.OfType<TourCardBox>())
            {
                card.ShowEditControls();
            }
        }

        private void EditModeChangeMit_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var card in TourBoxesSpl.Children.OfType<TourCardBox>())
            {
                card.HideEditControls();
            }
        }

        private void CreateNewTourCardMit_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TourCardEditerWnd();
            if (editWindow.ShowDialog() == true)
            {
                _viewModel.AddTourCard(editWindow.TourCard);
                UpdateCategoiesFiltersUI();
                ShowTourCards();
                UpdatePriceRangeUI();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FiltersModeMit != null)
                FiltersModeMit.IsChecked = !FiltersModeMit.IsChecked;
        }

        private void PriceRangeSelector_PriceRangeChanged(object sender, RoutedEventArgs e) => ShowTourCards();

        private void TourBoxesSpl_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (TourBoxesSpl.Children.Count > 0)
            {
                loadingTbx.Visibility = Visibility.Collapsed;
                searchNotFoundResultsTbx.Visibility = Visibility.Collapsed;
                return;
            }
            else if (TourBoxesSpl.Children.Count == 0 && !string.IsNullOrWhiteSpace(searchTbx.Text))
            {
                loadingTbx.Visibility = Visibility.Collapsed;
                searchNotFoundResultsTbx.Visibility = Visibility.Visible;
                return;
            }
            loadingTbx.Visibility = Visibility.Visible;
        }
    }
}