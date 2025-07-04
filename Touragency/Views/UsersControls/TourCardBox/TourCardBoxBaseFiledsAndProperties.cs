using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Touragency.Model;

namespace Touragency.Views.UsersControls
{
    public partial class TourCardBox : UserControl
    {
        public int TourId
        {
            get => _tourId;
            private set
            {
                _tourId = value;
            }
        }

        public string TourName
        {
            get => _tourName;
            set
            {
                _tourName = value ?? string.Empty;
                TourNameTbk.Text = _tourName;
            }
        }

        public string TourCity
        {
            get => _tourCity;
            set
            {
                _tourCity = value ?? string.Empty;
                TourCityTbk.Text = _tourCity;
            }
        }

        public ImageSource PreviewPicture
        {
            get => _previewPicture;
            set
            {
                _previewPicture = value;
                TourPreviewImg.Source = _previewPicture;
            }
        }
        private ImageSource BlackWhitePreviewPicture
        {
            get => (PreviewPicture == null) ? null : new FormatConvertedBitmap((BitmapSource)PreviewPicture, PixelFormats.Gray8, null, 0);
        }

        public uint TourCost
        {
            get => _tourCost;
            set
            {
                _tourCost = value;
                TourCostTbk.Text = FormatCost(value);
            }
        }

        public uint TicketsCount
        {
            get => _ticketsCount;
            set
            {
                _ticketsCount = value;
                TicketsCountTbk.Text = $"Билетов осталось: {_ticketsCount}";
            }
        }

        public string TourDescription
        {
            get => _tourDescription;
            set
            {
                _tourDescription = value ?? string.Empty;
                if (TourDescriptionTbx != null)
                    TourDescriptionTbx.Text = _tourDescription;
            }
        }

        public List<TourCategory> TourCategories
        {
            get => _tourCategories;
            set
            {
                _tourCategories = value;
            }
        }

        public bool TourIsActual
        {
            get => _tourIsActual;
            set
            {
                _tourIsActual = value;
                TourPreviewImg.Source = _tourIsActual ? PreviewPicture : BlackWhitePreviewPicture;
                if (_tourIsActual)
                {
                    TourNameTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    TourCostTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    TourCityTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    TicketsCountTbk.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    TourNameTbk.Text = _tourName;
                }
                else
                {
                    TourNameTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    TourCostTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    TourCityTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    TicketsCountTbk.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    TourNameTbk.Text = $"НЕАКТУАЛЬНЫЙ ТУР! {TourNameTbk.Text}";
                }
            }
        }

        private Model.TourCard _tourCard = new Model.TourCard();
        private int _tourId;
        private string _tourName = string.Empty;
        private string _tourDescription = string.Empty;
        private string _tourCity = string.Empty;
        private ImageSource _previewPicture;
        private uint _tourCost;
        private uint _ticketsCount;
        private List<Model.TourCategory> _tourCategories = new List<TourCategory>();
        private bool _tourIsActual;

        private string FormatCost(uint cost)
        {
            return cost.ToString("N0", CultureInfo.CurrentCulture) + " руб.";
        }
    }
}