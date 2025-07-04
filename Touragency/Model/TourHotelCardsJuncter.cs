using System.Collections.Generic;
using System.Linq;

namespace Touragency.Model
{
    public class TourHotelCardsJuncter
    {
        class TourHotelCardsPair
        {
            public HotelCard HotelCard {  get; private set; }
            public TourCard TourCard { get; private set; }
            public TourHotelCardsPair(TourCard tourCard, HotelCard hotelCard)
            {
                HotelCard = hotelCard;
                TourCard = tourCard;
            }
        }
        private static List<TourHotelCardsPair> _tourHotelCardsPairs = new List<TourHotelCardsPair>();
        public static IEnumerable<HotelCard> GetHotelCardsRelatedWithTour(TourCard tourCard)
        {
            if (tourCard is null)
            {
                return new HotelCard[0];
            }
            return _tourHotelCardsPairs.Where(th => th.TourCard.Id == tourCard.Id).Select(th => th.HotelCard);
        }

        public static IEnumerable<TourCard> GetTourCardsRelatedWithHotel(HotelCard hotelCard)
        {
            return _tourHotelCardsPairs.Where(th => th.HotelCard.Id == hotelCard.Id).Select(th => th.TourCard);
        }

        public static void AddNewRelation(TourCard tourCard, HotelCard hotelCard)
        {
            var pair = new TourHotelCardsPair(tourCard, hotelCard);
            if (_tourHotelCardsPairs.Contains(pair)) return;
            _tourHotelCardsPairs.Add(pair);
        }

        public static void DeleteRelation(TourCard tourCard, HotelCard hotelCard)
        {
            var pairs = _tourHotelCardsPairs.Where(p => p.TourCard.Name == tourCard.Name).Where(p => p.HotelCard.Name == hotelCard.Name);
            if (pairs.Any()) { 
                _tourHotelCardsPairs.Remove(pairs.First());
            }
        }
    }
}
