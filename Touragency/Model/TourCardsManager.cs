using System.Collections.Generic;
using System.Linq;

namespace Touragency.Model
{
    public static class TourCardsManager
    {
        private static IEnumerable<TourCard> _allTourCards;
        public static IEnumerable<TourCard> AllTourCards
        {
            get { return _allTourCards; }
            set
            {
                var newValue = value
                    .GroupBy(card => card.Name)
                    .Where(group => group.Count() == 1)
                    .SelectMany(group => group);
                _allTourCards = value;
            }
        }
        public static IEnumerable<string> AllRegistratedTourCategoriesNames { get; set; } = new List<string>();
    }
}
