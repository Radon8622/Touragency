using System;
using System.Collections.Generic;
using System.Linq;

namespace Touragency.Model
{
    public static class TourRecommender
    {
        const PriceRange PRICE_RANGE_DEFAULT = PriceRange.Medium;
        public static List<TourCard> GetRecommendedTours(IEnumerable<TourCard> allTours, Season season, PriceRange priceRange)
        {
            return allTours
                .OrderByDescending(t => CalculateTourRelevance(t, season, priceRange))
                .ToList();
        }
        public static List<TourCard> GetRecommendedTours(IEnumerable<TourCard> allTours)
        {
            Season season = Season.SpringAutumn;
            if (DateTime.Now.Month > 5 && DateTime.Now.Month < 9)
            {
                season = Season.Summer;
            }
            else if(DateTime.Now.Month < 3 || DateTime.Now.Month == 12)
            {
                season = Season.Winter;
            }
            return GetRecommendedTours(allTours, season, PRICE_RANGE_DEFAULT);
        }

        private static int CalculateTourRelevance(TourCard tour, Season season, PriceRange priceRange)
        {
            int relevanceScore = 0;
            relevanceScore += GetSeasonMatchScore(tour.Categories, season);
            relevanceScore += GetPriceMatchScore(tour.Price, priceRange);
            relevanceScore += GetAvailabilityScore(tour.TicketsCount);

            if (tour.Categories.Count > 1)
                relevanceScore += 20;

            return relevanceScore;
        }

        private static int GetSeasonMatchScore(List<TourCategory> categories, Season season)
        {
            foreach (var category in categories)
            {
                var categoryName = category.Value.ToLower();
                switch (season)
                {
                    case Season.Winter:
                        if (categoryName.Contains("лыж") || categoryName.Contains("сноуборд") || categoryName.Contains("Эльбрус") || categoryName.Contains("снеж"))
                            return 40;
                        if (categoryName.Contains("пляж") || categoryName.Contains("водопад") || categoryName.Contains("треккинг"))
                            return -40;
                        break;
                    case Season.Summer:
                        if (categoryName.Contains("пляж") || categoryName.Contains("водопад") || categoryName.Contains("треккинг"))
                            return 40;
                        if (categoryName.Contains("лыж") || categoryName.Contains("сноуборд") || categoryName.Contains("Эльбрус") || categoryName.Contains("снеж"))
                            return -40;
                        break;
                    case Season.SpringAutumn:
                        if (categoryName.Contains("городск") || categoryName.Contains("гастроном") || categoryName.Contains("винн"))
                            return 40;
                        if (categoryName.Contains("пляж") || categoryName.Contains("водопад") || categoryName.Contains("треккинг"))
                            return -20;
                        if (categoryName.Contains("лыж") || categoryName.Contains("сноуборд") || categoryName.Contains("Эльбрус") || categoryName.Contains("снеж"))
                            return -20;
                        break;
                }
            }
            return 0;
        }

        private static int GetPriceMatchScore(decimal cost, PriceRange priceRange)
        {
            if (priceRange == PriceRange.Budget && cost < 50000)
                return 30;
            if (priceRange == PriceRange.Medium && cost >= 50000 && cost <= 100000)
                return 30;
            if (priceRange == PriceRange.Premium && cost > 100000)
                return 30;
            return 0;
        }

        private static int GetAvailabilityScore(uint ticketsCount)
        {
            if (ticketsCount == 0)
            {
                ticketsCount = 1;
            }
            return Convert.ToInt32(Math.Sqrt(900d/ticketsCount));
        }
    }

    public enum Season { Winter, SpringAutumn, Summer }
    public enum PriceRange { Budget, Medium, Premium }
}