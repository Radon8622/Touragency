using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Touragency.Model;

namespace Touragency.Views.Windows
{
    public partial class TourCardEditerWnd : Window
    {
        private TourCardEditerViewModel _modelView;
        private bool _isCreateMode;
        private IEnumerable<string> _startCategories;
        public Model.TourCard TourCard => _modelView.TourCard;

        public TourCardEditerWnd(bool isCreateMode = true) : this(null, isCreateMode) { }

        public TourCardEditerWnd(Model.TourCard tourCard) : this(tourCard, false) { }

        private TourCardEditerWnd(Model.TourCard tourCard, bool isCreateMode)
        {
            _isCreateMode = isCreateMode;
            _startCategories = TourCardsManager.AllRegistratedTourCategoriesNames ?? new List<string>();
            _modelView = new TourCardEditerViewModel(tourCard);
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            var citiesNamesLoader = new Model.CitiesNamesFromInternalDictionaryLoader();
            TourCityCbx.ItemsSource = citiesNamesLoader.GetCitiesNames();

            foreach (var category in _startCategories)
            {
                tourTagsTipLbx.Items.Add(new ListBoxItem() { Content = category });
            }

            if (_modelView.TourCard != null)
            {
                SetTourCardData();
            }
            SetHotelsList();
        }

        private void SetTourCardData()
        {
            TourPreviewImg.Source = _modelView.ConvertToBitmapImage(_modelView.TourCard.CardPreview);
            TourNameTbx.Text = _modelView.TourCard.Name;
            TourDescriptionTbx.Text = _modelView.TourCard.Description;
            TourCityCbx.SelectedItem = _modelView.TourCard.City;
            TourCostTbx.Text = _modelView.TourCard.Price.ToString(CultureInfo.InvariantCulture);
            TicketsCountTbx.Text = _modelView.TourCard.TicketsCount.ToString(CultureInfo.InvariantCulture);
            TourTagsTbx.Text = _modelView.GetCategoriesTextFromList(_modelView.TourCard.Categories);
        }


        private void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^\\d+$");
        }

        private void NumericOnly_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!uint.TryParse(text, out _))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TourCostTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            _modelView.ValidateNumericRange(TourCostTbx, _modelView._minTourCost, _modelView._maxTourCost, "Стоимость тура");
        }

        private void TicketsCountTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            _modelView.ValidateNumericRange(TicketsCountTbx, _modelView._minTicketsCount, _modelView._maxTicketsCount, "Количество билетов");
        }

        private void CloseFormWithoutSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SaveAndCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_modelView.ValidateWritedCityName(TourCityCbx))
            {
                MessageBox.Show("Город не найден. Проверьте написание и повторите попытку.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_modelView.CheckTourNameIsUniqe(TourNameTbx.Text))
            {
                MessageBox.Show("Тур с таким именем уже существует. Измените название тура и повторите попытку.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _modelView.SaveTourCard(
                TourPreviewImg.Source as BitmapImage,
                TourNameTbx.Text,
                TourCityCbx.SelectedItem?.ToString(),
                TourCostTbx.Text,
                TicketsCountTbx.Text,
                TourDescriptionTbx.Text,
                TourTagsTbx.Text,
                GelAllHotelCardsNames());

            DialogResult = true;
            Close();
        }

        private void LoadNewCardPreviewBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadCardPreviewImage();
        }

        private void TourPreviewImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadCardPreviewImage();
        }

        private void LoadCardPreviewImage()
        {
            var imageSource = _modelView.LoadCardPreviewImage();
            if (imageSource != null)
            {
                TourPreviewImg.Source = imageSource;
                CheckCardFieldsIsWrited();
            }
        }

        private void TourNameTbx_TextChanged(object sender, TextChangedEventArgs e) => CheckCardFieldsIsWrited();
        private void TourDescriptionTbx_TextChanged(object sender, TextChangedEventArgs e) => CheckCardFieldsIsWrited();
        private void TourCityCbx_SelectionChanged(object sender, SelectionChangedEventArgs e) => CheckCardFieldsIsWrited();

        private bool CheckCardFieldsIsWrited()
        {
            bool result = !string.IsNullOrWhiteSpace(TourNameTbx.Text) &&
                          TourCityCbx.SelectedItem != null &&
                          TourPreviewImg.Source != null;

            SaveAndCloseBtn.IsEnabled = result;
            return result;
        }

        private void TourTagsTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            tourTagsTipSrlv.Visibility = Visibility.Visible;
            CheckCardFieldsIsWrited();
        }

        private void TourTagsTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            tourTagsTipSrlv.Visibility = Visibility.Collapsed;
            CheckCardFieldsIsWrited();
        }

        private void TourTagsTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            SyncCategoryListWithText();
        }

        private void tourTagsTipLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SyncTextWithCategoryList();
        }

        private void SyncCategoryListWithText()
        {
            var writedCategories = _modelView.GetCategoriesFromText(TourTagsTbx.Text);

            foreach (ListBoxItem item in tourTagsTipLbx.Items)
            {
                string categoryName = item.Content.ToString();
                item.IsSelected = writedCategories.Any(wc => wc.Value == categoryName);
            }

            CheckCardFieldsIsWrited();
        }

        private void SyncTextWithCategoryList()
        {
            var selectedCategories = tourTagsTipLbx.SelectedItems
                .OfType<ListBoxItem>()
                .Select(item => item.Content.ToString())
                .Distinct()
                .ToList();

            var textCategories = _modelView.GetCategoriesFromText(TourTagsTbx.Text);
            var resultCategories = textCategories.Where(c => selectedCategories.Contains(c.Value)).ToList();

            foreach (var cat in selectedCategories)
            {
                if (!resultCategories.Any(c => c.Value == cat))
                {
                    resultCategories.Add(new TourCategory() { Value = cat });
                }
            }

            TourTagsTbx.Text = _modelView.GetCategoriesTextFromList(resultCategories);
            CheckCardFieldsIsWrited();
        }
        private void SetHotelsList()
        {
            foreach (var hotelCard in HotelCardsManager.AllHotelCards)
            {
                HotelsLbx.Items.Add(hotelCard.Name);
                if (TourHotelCardsJuncter.GetHotelCardsRelatedWithTour(TourCard).Where(h => h.Id == hotelCard.Id).Any())
                {
                    HotelsLbx.SelectedItems.Add(HotelsLbx.Items[HotelsLbx.Items.Count-1]);
                }
            }
        }
        private IEnumerable<string> GelAllHotelCardsNames()
        {
            List<string> names = new List<string>();
            foreach (var item in HotelsLbx.SelectedItems)
            {
                if (item is string name)
                {
                    names.Add(name);
                }
            }
            return names;
        }
    }
}
