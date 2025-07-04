using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Touragency.Model;

namespace Touragency.Views.UsersControls
{
    public partial class HotelCardBox : UserControl
    {
        public int HotelId
        {
            get => _hotelId;
            private set
            {
                _hotelId = value;
            }
        }

        public string HotelName
        {
            get => _hotelName;
            set
            {
                _hotelName = value ?? string.Empty;
                HotelNameTbk.Text = _hotelName;
            }
        }

        public string HotelCity
        {
            get => _hotelCity;
            set
            {
                _hotelCity = value ?? string.Empty;
                HotelCityTbk.Text = _hotelCity;
            }
        }

        public ImageSource PreviewPicture
        {
            get => _previewPicture;
            set
            {
                _previewPicture = value;
                HotelPreviewImg.Source = _previewPicture;
            }
        }
        public string PreviewImagePath
        {
            get => _previewImagePath;
            set
            {
                _previewImagePath = value;
                HotelPreviewImg.Source = _previewPicture;
            }
        }
        private ImageSource BlackWhitePreviewPicture
        {
            get => (PreviewPicture == null) ? null : new FormatConvertedBitmap((BitmapSource)PreviewPicture, PixelFormats.Gray8, null, 0);
        }

        public uint StarsCount
        {
            get => _starsCount;
            set
            {
                _starsCount = value;
                starsCountControl.StarCount = (int)Math.Clamp(_starsCount, 0, 5);
            }
        }

        public bool HotelIsActual
        {
            get => _hotelIsActual;
            set
            {
                _hotelIsActual = value;
                HotelPreviewImg.Source = _hotelIsActual ? PreviewPicture : BlackWhitePreviewPicture;
                if (_hotelIsActual)
                {
                    HotelNameTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    HotelCityTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    HotelNameTbk.Text = _hotelName;
                }
                else
                {
                    HotelNameTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    HotelCityTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    HotelNameTbk.Text = $"НЕАКТУАЛЬНЫЙ ОТЕЛЬ! {HotelNameTbk.Text}";
                }
            }
        }

        private Model.HotelCard _hotelCard = new Model.HotelCard();
        private int _hotelId;
        private string _hotelName = string.Empty;
        private string _hotelCity = string.Empty;
        private ImageSource _previewPicture;
        private string _previewImagePath;
        private uint _starsCount;
        private bool _hotelIsActual;

        private async Task SetPreviewPictureAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;

            var image = await Task.Run(() =>
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return (ImageSource)bitmap;
                }
                catch
                {
                    return null;
                }
            });

            if (image != null)
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    _previewPicture = image;
                    HotelPreviewImg.Source = _previewPicture;
                });
            }
        }
    }
}