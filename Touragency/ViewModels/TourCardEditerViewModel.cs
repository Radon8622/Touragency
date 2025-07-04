using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Touragency.Model;

namespace Touragency.Views.Windows
{
    public class TourCardEditerViewModel
    {
        // Константы для диапазонов и фильтров
        public uint _minTourCost = 5000;
        public uint _maxTourCost = 500000;
        public uint _minTicketsCount = 0;
        public uint _maxTicketsCount = 150;
        private string _imageFilter = "Изображения|*.png;*.bmp;*.jpg;*.jpeg";
        private IEnumerable<Model.TourCard> _allTourCards;

        public Model.TourCard TourCard { get; private set; }

        public TourCardEditerViewModel(Model.TourCard tourCard)
        {
            TourCard = tourCard;
            _allTourCards = Model.TourCardsManager.AllTourCards;
        }

        public void ValidateNumericRange(TextBox textBox, uint minValue, uint maxValue, string fieldName)
        {
            if (!uint.TryParse(textBox.Text, out uint value))
            {
                textBox.Text = minValue.ToString(CultureInfo.InvariantCulture);
                return;
            }

            if (value < minValue || value > maxValue)
            {
                MessageBox.Show($"{fieldName} должно быть в диапазоне [{minValue}, {maxValue}].", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                textBox.Text = Math.Clamp(value, minValue, maxValue).ToString(CultureInfo.InvariantCulture);
            }
        }

        public bool ValidateWritedCityName(ComboBox comboBox)
        {
            return comboBox.Items.Cast<string>().Where(c => c == comboBox.Text).Any();
        }

        public void SaveTourCard(BitmapImage previewImage, string name, string city, string cost, string ticketsCount, string description, string tagsText, IEnumerable<string> hotelsNames)
        {

            var tags = GetCategoriesFromText(tagsText);
            if (TourCard is null)
            {
                TourCard = new TourCard
                {
                    CardPreview = ConvertToImageSource(previewImage),
                    Name = name,
                    City = city,
                    Price = uint.Parse(cost),
                    TicketsCount = uint.Parse(ticketsCount),
                    Description = description,
                    Categories = tags,
                    IsActual = TourCard?.IsActual ?? true
                };
            }
            else
            {
                TourCard = new TourCard
                {
                    Id = TourCard.Id,
                    CardPreview = ConvertToImageSource(previewImage),
                    Name = name,
                    City = city,
                    Price = uint.Parse(cost),
                    TicketsCount = uint.Parse(ticketsCount),
                    Description = description,
                    Categories = tags,
                    IsActual = TourCard?.IsActual ?? true
                };
            }
            List<HotelCard> cards = new List<HotelCard>();
            foreach (var hotelName in hotelsNames) 
            {
                var hotel = HotelCardsManager.AllHotelCards.First(h => h.Name == hotelName);
                cards.Add(hotel);
                if (!TourHotelCardsJuncter.GetHotelCardsRelatedWithTour(TourCard).Contains(hotel))
                {
                    TourHotelCardsJuncter.AddNewRelation(TourCard, hotel);
                }
            }
            while (true)
            {
                try
                {
                    foreach (var hotel in TourHotelCardsJuncter.GetHotelCardsRelatedWithTour(TourCard))
                    {
                        if (!cards.Where(c => c.Name == hotel.Name).Any())
                        {
                            TourHotelCardsJuncter.DeleteRelation(TourCard, hotel);
                        }
                    }
                    break;
                }
                catch (Exception) { }
            }
        }
        public List<TourCategory> GetCategoriesFromText(string text)
        {
            List<Model.TourCategory> tags = new List<Model.TourCategory>();
            foreach (var item in text.Split(',', ';'))
            {
                var strTag = item.Trim();
                if (strTag.Length == 0) continue;
                tags.Add(new Model.TourCategory() { Value = strTag });
            }
            return tags;
        }

        public string GetCategoriesTextFromList(IEnumerable<TourCategory> tourCategories)
        {

            string tagsText = string.Empty;

            for (int i = 0; i < tourCategories.Count(); i++)
            {
                tagsText += tourCategories.ElementAt(i).Value;
                if (i < tourCategories.Count() - 1) tagsText += ", ";
            }

            return tagsText.Trim();
        }

        public BitmapImage ConvertToBitmapImage(ImageSource imageSource)
        {
            return imageSource is BitmapImage bitmapImage ? bitmapImage : EncodeImage(imageSource);
        }

        public ImageSource ConvertToImageSource(BitmapImage bitmapImage)
        {
            return bitmapImage ?? EncodeImage(bitmapImage);
        }

        private BitmapImage EncodeImage(ImageSource imageSource)
        {
            if (imageSource == null) return null;

            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageSource as BitmapSource));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = ms;
                result.EndInit();
                return result;
            }
        }

        public BitmapImage LoadCardPreviewImage()
        {
            var openFileDialog = new OpenFileDialog { Filter = _imageFilter };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    return new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось загрузить изображение карточки тура. Возможно, выбранный файл вовсе не является изображением.", "Ошибка загрузки файла");
                }
            }
            return null;
        }

        public bool CheckTourNameIsUniqe(string tourName)
        {
            if (_allTourCards is null)
                return true;
            if (TourCard is null)
            {
                return !_allTourCards.Where(t => t.Name == tourName).Any();
            }
            return !_allTourCards.Where(t => t.Name == tourName && t.Id != TourCard.Id).Any();
        }
    }
}