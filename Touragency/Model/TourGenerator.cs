using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Touragency.Model
{
    public class TourGenerator
    {
        public static List<TourCard> GenerateSampleTours()
        {
            return new List<TourCard>
            {
                new TourCard
                {
                    Id = 0,
                    Name = "Кремль и Красная площадь",
                    City = "Москва",
                    Price = 35000,
                    TicketsCount = 100,
                    Description = "Экскурсия по главным достопримечательностям Москвы с посещением Собора Василия Блаженного и Армейского музея. " +
                                  "Программа включает прогулку по территории Кремля, осмотр древних соборов и возможность сфотографироваться на фоне знаменитых курантов. " +
                                  "Вы также сможете узнать историю Московского государства и современной России.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\kremlin.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Historical,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 1,
                    Name = "Петергоф и Петербург",
                    City = "Санкт-Петербург",
                    Price = 45000,
                    TicketsCount = 80,
                    Description = "Путешествие по императорским резиденциям Санкт-Петербурга с посещением фонтанов Петергофа. " +
                                  "В программе: прогулка по Большому дворцу, осмотр Нижнего парка и уникальных золотых фонтанов. " +
                                  "Дополнительно вы сможете исследовать исторический центр города, включая Исаакиевский собор, Эрмитаж и Дворцовую площадь.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\peterhof.png", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Historical,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 2,
                    Name = "Тур по Золотому Кольцу",
                    City = "Суздаль",
                    Price = 75000,
                    TicketsCount = 50,
                    Description = "Автобусный тур по древним городам Золотого Кольца России: Владимир, Суздаль, Ростов Великий. " +
                                  "Программа включает посещение Успенского собора, музей деревянного зодчества и дегустацию местных напитков. " +
                                  "Вы погрузитесь в атмосферу старинной Руси, увидите уникальные храмы и узнаете истории этих мест.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\suzdal.png", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Historical,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 3,
                    Name = "Байкальские просторы",
                    City = "Иркутск",
                    Price = 120000,
                    TicketsCount = 30,
                    Description = "Треккинг по окрестностям Байкала с проживанием в экодеревне. " +
                                  "В программе: прогулка по Ольхону, посещение Шаманской скалы, экскурсии по живописным бухтам и дегустация байкальской рыбы. " +
                                  "Вы сможете насладиться первозданной природой, встретить рассвет над озером и почувствовать себя частью этого удивительного мира.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\baikal.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Nature,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Adventure
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 4,
                    Name = "Казанский кремль",
                    City = "Казань",
                    Price = 40000,
                    TicketsCount = 120,
                    Description = "Пешеходная экскурсия с посещением мечети Кул-Шариф и Благовещенского собора. " +
                                  "Вы узнаете об истории Казанского ханства, увидите уникальное сочетание культур и архитектуры разных народов. " +
                                  "Программа включает прогулку по территории кремля и рассказ о его роли в истории России.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\kazan.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Historical,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 5,
                    Name = "Сочинский национальный парк",
                    City = "Сочи",
                    Price = 95000,
                    TicketsCount = 60,
                    Description = "Экологический тур с посещением водопадов и Кавказского заповедника. " +
                                  "В программе: прогулки по тропам, знакомство с флорой и фауной региона, а также отдых на лоне природы. " +
                                  "Вы сможете увидеть уникальные виды горных ландшафтов и искупаться в чистейших реках.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\sochi.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Nature,
                        PredefinedTourCategories.Beach,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Wellness
                    },
                    IsActual = false
                },
                new TourCard
                {
                    Id = 6,
                    Name = "Гастротур по Кавказу",
                    City = "Кисловодск",
                    Price = 68000,
                    TicketsCount = 25,
                    Description = "Знакомство с кухней народов Кавказа: хычины, шашлык, местные сыры и травяные чаи. " +
                                  "В программе: мастер-классы по приготовлению блюд, дегустации и посещение местных рынков. " +
                                  "Вы сможете не только попробовать уникальные блюда, но и узнать их историю.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\gastrovkaz.png", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Gastronomy,
                        PredefinedTourCategories.Thematic
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 7,
                    Name = "Семейный отдых в Крыму",
                    City = "Ялта",
                    Price = 85000,
                    TicketsCount = 40,
                    Description = "Пляжный отдых с детскими развлечениями и экскурсиями. " +
                                  "В программе: прогулки по набережной, посещение аквапарка, экскурсии в Ливадийский и Воронцовский дворцы. " +
                                  "Идеальный вариант для семейного отдыха с комфортом и разнообразием активностей.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\livadiysky.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Beach,
                        PredefinedTourCategories.Family,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 8,
                    Name = "Винный тур в Крым",
                    City = "Севастополь",
                    Price = 92000,
                    TicketsCount = 20,
                    Description = "Дегустации в лучших винодельнях Крыма с экскурсиями. " +
                                  "В программе: посещение виноградников, знакомство с технологиями производства вина и дегустации лучших сортов. " +
                                  "Вы сможете насладиться потрясающими видами на виноградники и узнать секреты создания крымских вин.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\inkerman.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Gastronomy,
                        PredefinedTourCategories.Luxury
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 9,
                    Name = "Восхождение на Эльбрус",
                    City = "Эльбрус",
                    Price = 150000,
                    TicketsCount = 15,
                    Description = "Горный поход с профессиональными гидами и проживанием в приютах. " +
                                  "В программе: восхождение на вершину Европы, обучение основам альпинизма и незабываемые виды на Кавказские горы. " +
                                  "Это путешествие станет настоящим испытанием и приключением для любителей активного отдыха.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\elbrus.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Adventure,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Nature
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 10,
                    Name = "Путешествие к центру мира",
                    City = "Энем",
                    Price = 75000,
                    TicketsCount = 1,
                    Description = "Откройте для себя Энем – место, где старинные улочки дышат историей, а современный ритм жизни сливается с природной красотой. " +
                                  "В программе: прогулки по историческим местам, знакомство с местной культурой и дегустация традиционных блюд. " +
                                  "Это путешествие подарит вам уникальные впечатления и незабываемые эмоции.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\enemParadise.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Gastronomy,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Adventure
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 11,
                    Name = "Экскурсия по древнему Дербенту",
                    City = "Дербент",
                    Price = 35000,
                    TicketsCount = 20,
                    Description = "Путешествие в один из старейших городов России с посещением древней крепости Нарын-Кала. " +
                                  "В программе: прогулка по узким улочкам, осмотр древних стен и башен, а также знакомство с историей этого уникального места. " +
                                  "Вы почувствуете, как время словно остановилось в этом городе.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\derbent.png", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Historical,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Sightseeing
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 12,
                    Name = "Лечебные источники Алтая",
                    City = "Горно-Алтайск",
                    Price = 60000,
                    TicketsCount = 15,
                    Description = "Оздоровительный тур с посещением минеральных источников и горных ландшафтов Алтая. " +
                                  "В программе: спа-процедуры, прогулки по горным тропам и отдых на свежем воздухе. " +
                                  "Вы сможете восстановить силы и зарядиться энергией в окружении первозданной природы.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\gorno-altaisk.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Wellness,
                        PredefinedTourCategories.Nature
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 13,
                    Name = "Горнолыжный отдых в Шерегеше",
                    City = "Шерегеш",
                    Price = 80000,
                    TicketsCount = 25,
                    Description = "Зимний отдых с катанием на склонах одного из лучших горнолыжных курортов России. " +
                                  "В программе: трассы для новичков и профессионалов, вечерние развлекательные мероприятия и уютные рестораны. " +
                                  "Вы сможете насладиться горнолыжным сезоном в полной мере.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\shegeresh.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Ski,
                        PredefinedTourCategories.Urban,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Adventure
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 14,
                    Name = "Детский лагерь 'Орленок'",
                    City = "Туапсе",
                    Price = 45000,
                    TicketsCount = 50,
                    Description = "Летний отдых для детей с развлекательными программами и спортивными мероприятиями. " +
                                  "В программе: спортивные игры, мастер-классы, дискотеки и квесты. " +
                                  "Дети смогут найти новых друзей и весело провести время под присмотром опытных вожатых.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\olenok.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Child
                    },
                    IsActual = true
                },
                new TourCard
                {
                    Id = 15,
                    Name = "Корпоративный тимбилдинг в Карелии",
                    City = "Петрозаводск",
                    Price = 150000,
                    TicketsCount = 10,
                    Description = "Командное путешествие с активными играми, прогулками по озерам и ночёвкой в палатках. " +
                                  "В программе: сплочение команды через природные приключения, костры под звездами и общие задачи. " +
                                  "Это идеальный способ укрепить корпоративный дух в живописной Карелии.",
                    CardPreview = new BitmapImage(new Uri(@$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\TourPreviews\karelsky_vedi.jpg", UriKind.Absolute)),
                    Categories = new List<TourCategory>()
                    {
                        PredefinedTourCategories.Corporate,
                        PredefinedTourCategories.Luxury,
                        PredefinedTourCategories.Nature
                    },
                    IsActual = true
                }
            };
        }
    }
}