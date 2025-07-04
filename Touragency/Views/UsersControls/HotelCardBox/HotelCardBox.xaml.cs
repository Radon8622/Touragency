using System.Windows.Controls;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows;
using Touragency.Model;
using Touragency.Views.Pages;
using Touragency.Views.Windows;


namespace Touragency.Views.UsersControls
{
    public partial class HotelCardBox : UserControl
    {
        public HotelCardBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        public HotelCardBox(HotelCard hotelCard) : this()
        {
            InitializeHotelCard(hotelCard);
        }

        private async Task InitializeHotelCard(HotelCard hotelCard)
        {
            if (hotelCard == null)
            {
                hotelCard = new HotelCard();
            }

            PreviewImagePath = hotelCard.PreviewImagePath;
            SetPreviewPictureAsync(PreviewImagePath);
            HotelName = hotelCard.Name;
            HotelCity = hotelCard.City;
            StarsCount = hotelCard.StarsCount;
            HotelId = hotelCard.Id;

            UpdateHotelCard();
        }

        private void UpdateHotelCard()
        {
            _hotelCard.Id = HotelId;
            _hotelCard.PreviewImagePath = PreviewImagePath;
            _hotelCard.Name = HotelName;
            _hotelCard.City = HotelCity;
            _hotelCard.StarsCount = StarsCount;
        }
        public ImageSource PreviewImage
        {
            get => HotelPreviewImg.Source;
            set => HotelPreviewImg.Source = value;
        }
    }
}