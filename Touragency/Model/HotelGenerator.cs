using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Touragency.Model
{
    public class HotelGenerator
    {
        public static List<HotelCard> GenerateSampleHotels()
        {
            var hotels = new List<HotelCard>()
            {
                new HotelCard { Id = 1, Name = "Гранд Москва", City = "Москва", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\1.jpg" },
                new HotelCard { Id = 2, Name = "Hotel Kremlin View", City = "Москва", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\2.png" },

                new HotelCard { Id = 3, Name = "Fontaine Palace", City = "Санкт-Петербург", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\3.jpg" },
                new HotelCard { Id = 4, Name = "Петербургский уют", City = "Санкт-Петербург", StarsCount = 3, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\4.jpg" },

                new HotelCard { Id = 5, Name = "Купеческий двор", City = "Суздаль", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\5.jpg" },
                new HotelCard { Id = 6, Name = "Zolotoy Koltso Inn", City = "Суздаль", StarsCount = 3, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\6.jpg" },

                new HotelCard { Id = 7, Name = "Baikal Eco Lodge", City = "Иркутск", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\7.jpg" },

                new HotelCard { Id = 8, Name = "Kazan Palace", City = "Казань", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\8.jpg" },
                new HotelCard { Id = 9, Name = "Kul Sharif Inn", City = "Казань", StarsCount = 3, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\9.jpg" },

                new HotelCard { Id = 10, Name = "Sochi Park Hotel", City = "Сочи", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\10.jpg" },
                new HotelCard { Id = 11, Name = "Caucasus Riviera", City = "Сочи", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\11.jpg" },

                new HotelCard { Id = 12, Name = "Le Petit Délice", City = "Кисловодск", StarsCount = 3, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\12.jpg" },
                new HotelCard { Id = 13, Name = "Del Gusto Hotel", City = "Кисловодск", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\13.jpg" },

                new HotelCard { Id = 14, Name = "Yalta Resort & Spa", City = "Ялта", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\14.jpg" },
                new HotelCard { Id = 15, Name = "Familia Maris", City = "Ялта", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\15.jpg" },

                new HotelCard { Id = 16, Name = "Cantina Sevastopol", City = "Севастополь", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\16.jpg" },

                new HotelCard { Id = 17, Name = "Mount Elbrus Lodge", City = "Эльбрус", StarsCount = 3, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\17.jpg" },

                new HotelCard { Id = 18, Name = "Enem Boutique Hotel", City = "Энем", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\18.jpg" },

                new HotelCard { Id = 19, Name = "Naryn Heritage", City = "Дербент", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\19.jpg" },

                new HotelCard { Id = 20, Name = "Altai Hills Retreat", City = "Горно-Алтайск", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\20.jpg" },

                new HotelCard { Id = 21, Name = "Sheregesh Snowline", City = "Шерегеш", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\21.jpg" },
                new HotelCard { Id = 22, Name = "Gornaya Vershina", City = "Шерегеш", StarsCount = 4, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\22.jpg" },

                new HotelCard { Id = 23, Name = "Northern Escape", City = "Петрозаводск", StarsCount = 5, PreviewImagePath = @$"{App.PROGRAM_RESOURCES_DIRICTORY}\Images\HotelPreviews\23.jpg" }
            };

            return hotels;
        }
    }
}