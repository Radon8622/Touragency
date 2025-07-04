using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Touragency.Model
{
    public class HotelCard
    {
        private int _id;
        public int Id
        { 
            get { return _id; }
            set
            {
                _id = value;
                if (Id < 0)
                {
                    if (HotelCardsManager.AllHotelCards == null)
                    {
                        _id = 0;
                        return;
                    }
                    if (HotelCardsManager.AllHotelCards.Count() == 0)
                    {
                        _id = 0;
                        return;
                    }
                    int newId = HotelCardsManager.AllHotelCards.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
                    _id = newId;
                }
            }
        }
        public string PreviewImagePath { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public uint StarsCount { get; set; }

        private BitmapImage _cachedImage;
        public BitmapImage CachedImage
        {
            get
            {
                if (_cachedImage == null && !string.IsNullOrEmpty(PreviewImagePath))
                {
                    _cachedImage = new BitmapImage(new Uri(PreviewImagePath, UriKind.RelativeOrAbsolute));
                    _cachedImage.Freeze(); // Optimize for UI thread usage
                }
                return _cachedImage;
            }
        }

        public HotelCard()
        {
            Id = -1;
        }
        public HotelCard(Views.UsersControls.HotelCardBox hotelCardBox) : this()
        {
            Id = hotelCardBox.HotelId;
            PreviewImagePath = hotelCardBox.PreviewImagePath;
            Name = hotelCardBox.HotelName;
            City = hotelCardBox.HotelCity;
            StarsCount = hotelCardBox.StarsCount;
        }
    }
}
