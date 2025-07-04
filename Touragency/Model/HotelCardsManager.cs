using System.Collections.Generic;
using System.Linq;

namespace Touragency.Model
{
    public static class HotelCardsManager
    {
        private static IEnumerable<HotelCard> _allHotelCards;
        public static IEnumerable<HotelCard> AllHotelCards
        {
            get { return _allHotelCards; }
            set
            {
                var newValue = value
                    .GroupBy(card => card.Name)
                    .Where(group => group.Count() == 1)
                    .SelectMany(group => group);
                _allHotelCards = value;
            }
        }
        public static IEnumerable<string> AllRegistratedTourCategoriesNames { get; set; } = new List<string>();
    }
}
