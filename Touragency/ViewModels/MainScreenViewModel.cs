using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Touragency.Model;
using Touragency.Views.Pages;
using Touragency.Views.Windows;

namespace Touragency.ViewModels
{
    public class MainScreenViewModel
    {
        private List<TourCard> _tourCardsList;
        private List<HotelCard> _hotelCardsList;
        private HotelPage _hotelPage;
        private ToursPage _tourPage;

        private TabControl _tabPages;
        private TabItem _tourPageTbi;
        private TabItem _hotelPageTbi;
        public MainScreenViewModel(MainscreenWnd sender)
        {
            _tabPages = sender.tabPages;
            _tourPageTbi = sender.tourPageTbi;
            _hotelPageTbi = sender.hotelPageTbi;

            _tourCardsList = TourGenerator.GenerateSampleTours();
            _hotelCardsList = HotelGenerator.GenerateSampleHotels();

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[0], _hotelCardsList[0]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[0], _hotelCardsList[1]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[1], _hotelCardsList[2]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[1], _hotelCardsList[3]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[2], _hotelCardsList[4]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[2], _hotelCardsList[5]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[3], _hotelCardsList[6]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[4], _hotelCardsList[7]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[4], _hotelCardsList[8]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[5], _hotelCardsList[9]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[5], _hotelCardsList[10]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[6], _hotelCardsList[11]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[6], _hotelCardsList[12]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[7], _hotelCardsList[13]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[7], _hotelCardsList[14]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[8], _hotelCardsList[15]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[9], _hotelCardsList[16]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[10], _hotelCardsList[17]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[11], _hotelCardsList[18]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[12], _hotelCardsList[19]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[13], _hotelCardsList[20]);
            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[13], _hotelCardsList[21]);

            TourHotelCardsJuncter.AddNewRelation(_tourCardsList[15], _hotelCardsList[22]);

            _tourPage = new ToursPage();
            _hotelPage = new HotelPage();
        }
        public ToursPage GetToursPage()
        {
            foreach (var tourCard in _tourCardsList)
            {
                _tourPage.TourCards.Add(tourCard);
            }

            return _tourPage;
        }
        public HotelPage GetHotelsPage()
        {
            foreach (var hotelCard in _hotelCardsList)
            {
                _hotelPage.HotelsCards.Add(hotelCard);
            }

            return _hotelPage;
        }

        public bool ConfirmLogout()
        {
            return MessageBox.Show(
                "Вы уверены, что хотите выйти из вашей учетной записи?",
                "Выход из системы",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public void OpenHotelPageAndInsertSearchText(string searchText)
        {
            if (_tourPageTbi.IsSelected == true)
            {
                _tourPageTbi.IsSelected = false;
            }
            if (_hotelPageTbi.IsSelected == false)
            {
                _hotelPageTbi.IsSelected = true;
            }
            _hotelPage.searchTbx.Text = searchText;
        }
    }
}