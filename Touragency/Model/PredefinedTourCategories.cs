namespace Touragency.Model
{
    public static class PredefinedTourCategories
    {
        public static TourCategory Historical => new TourCategory { Value = "Культурно-исторические туры" };
        public static TourCategory Nature => new TourCategory { Value = "Природные и экологические туры" };
        public static TourCategory Beach => new TourCategory { Value = "Пляжные и курортные туры" };
        public static TourCategory Adventure => new TourCategory { Value = "Активные и приключенческие туры" };
        public static TourCategory Gastronomy => new TourCategory { Value = "Гастрономические и винные туры" };
        public static TourCategory Urban => new TourCategory { Value = "Городские туры и weekend-путешествия" };
        public static TourCategory Thematic => new TourCategory { Value = "Тематические туры" };
        public static TourCategory Luxury => new TourCategory { Value = "Роскошные и VIP-туры" };
        public static TourCategory Family => new TourCategory { Value = "Семейные туры" };
        public static TourCategory Child => new TourCategory { Value = "Специализированные детские туры" };
        public static TourCategory Wellness => new TourCategory { Value = "Лечебно-оздоровительные туры" };
        public static TourCategory Sightseeing => new TourCategory { Value = "Экскурсионные туры" };
        public static TourCategory Corporate => new TourCategory { Value = "Обслуживание корпоративных клиентов по заказу" };
        public static TourCategory Ski => new TourCategory { Value = "Горнолыжные курорты" };
    }
}
