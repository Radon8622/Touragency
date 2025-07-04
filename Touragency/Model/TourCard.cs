using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Touragency.Model
{
    public class TourCard
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
                    if (TourCardsManager.AllTourCards == null)
                    {
                        _id = 0;
                        return;
                    }
                    if (TourCardsManager.AllTourCards.Count() == 0)
                    {
                        _id = 0;
                        return;
                    }
                    int newId = TourCardsManager.AllTourCards.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
                    _id = newId;
                }
            }
        }
        public ImageSource CardPreview { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public uint Price { get; set; }
        public uint TicketsCount { get; set; }
        public string Description { get; set; }
        public List<TourCategory> Categories { get; set; } = new List<TourCategory>();
        public bool IsActual;

        private BitmapImage _cachedImage;
        public BitmapImage CachedImage
        {
            get
            {
                if (_cachedImage == null && CardPreview is BitmapImage bitmapImage)
                {
                    _cachedImage = bitmapImage;
                    _cachedImage.Freeze(); // Optimize for UI thread usage
                }
                return _cachedImage;
            }
        }

        public TourCard()
        {
            Id = -1;
        }
        public TourCard(Views.UsersControls.TourCardBox tourCardBox) : this()
        {
            Id = tourCardBox.TourId;
            CardPreview = tourCardBox.PreviewPicture;
            Name = tourCardBox.TourName;
            City = tourCardBox.TourCity;
            Price = tourCardBox.TourCost;
            TicketsCount = tourCardBox.TicketsCount;
            Description = tourCardBox.TourDescription;
            Categories = tourCardBox.TourCategories;
            IsActual = tourCardBox.TourIsActual;
        }
    }
}
