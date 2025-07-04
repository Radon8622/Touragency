using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Touragency.Model;
using Touragency.Views.UsersControls;

namespace Touragency.ViewModels
{
    public class HotelsViewModel : INotifyPropertyChanged
    {
        private readonly object _lock = new object();
        private CancellationTokenSource _searchCts;
        private ObservableCollection<HotelCard> _observableAllHotelsCards;

        public ObservableCollection<HotelCard> HotelCards
        {
            get
            {
                if (_observableAllHotelsCards is null)
                {
                    _observableAllHotelsCards = HotelCardsManager.AllHotelCards != null
                        ? new ObservableCollection<HotelCard>(HotelCardsManager.AllHotelCards)
                        : new ObservableCollection<HotelCard>();
                }
                HotelCardsManager.AllHotelCards = _observableAllHotelsCards;
                return _observableAllHotelsCards;
            }
            set
            {
                HotelCardsManager.AllHotelCards = value;
                if (_observableAllHotelsCards == null)
                {
                    _observableAllHotelsCards = new ObservableCollection<HotelCard>(value);
                }
                else
                {
                    var added = value.Except(_observableAllHotelsCards).ToList();
                    foreach (var tourCard in added)
                    {
                        _observableAllHotelsCards.Add(tourCard);
                    }
                }
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadHotelCardsAsync(IEnumerable<HotelCardBox> cardBoxes)
        {
            try
            {
                var loadedCards = await Task.Run(() =>
                {
                    var tempCollection = new List<HotelCard>();
                    foreach (var cardBox in cardBoxes)
                    {
                        var card = new HotelCard(cardBox);
                        if (!tempCollection.Any(t => t.Id == card.Id))
                            tempCollection.Add(card);
                    }
                    return tempCollection;
                });

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    lock (_lock)
                    {
                        HotelCards.Clear();
                        foreach (var card in loadedCards)
                            HotelCards.Add(card);
                    }
                });
            }
            catch (TaskCanceledException) { }
        }

        public async Task LoadHotelCardsAsync()
        {
            List<HotelCardBox> cardBoxes;
            lock (_lock)
            {
                cardBoxes = HotelCards.Select(c => new HotelCardBox(c)).ToList();
            }
            await LoadHotelCardsAsync(cardBoxes);
        }

        public async Task<IEnumerable<HotelCard>> GetFilteredHotelCardsAsync(
            string searchText,
            bool isLowStarsSort,
            bool isHighStarsSort)
        {
            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();

            try
            {
                var lowerSearchText = searchText.ToLowerInvariant();

                return await Task.Run(() =>
                {
                    _searchCts.Token.ThrowIfCancellationRequested();
                    return GetHotelsAppliedFiltersAndSearch(
                        HotelCards,
                        lowerSearchText,
                        isLowStarsSort,
                        isHighStarsSort);
                }, _searchCts.Token);
            }
            catch (TaskCanceledException) { return Enumerable.Empty<HotelCard>(); }
            catch (ObjectDisposedException) { return Enumerable.Empty<HotelCard>(); }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return Enumerable.Empty<HotelCard>();
            }
        }

        private IEnumerable<HotelCard> GetHotelsAppliedFiltersAndSearch(
            IEnumerable<HotelCard> hotelCards,
            string searchText,
            bool isLowStarsSort,
            bool isHighStarsSort)
        {
            var filtered = GetHotelsCardMeetsSearchCriteria(hotelCards, searchText);

            return GetHotelsCardSorted(filtered, isLowStarsSort, isHighStarsSort);
        }

        private IEnumerable<HotelCard> GetHotelsCardMeetsSearchCriteria(
            IEnumerable<HotelCard> hotelCards, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) { 
                return hotelCards;
            }
            return hotelCards.Where(c =>
                c.Name.ToLowerInvariant().Contains(searchText) ||
                c.City.ToLowerInvariant().Contains(searchText) ||
                TourHotelCardsJuncter.GetTourCardsRelatedWithHotel(c).Any(t => t.Name.ToLowerInvariant().Contains(searchText)));
        }

        private IEnumerable<HotelCard> GetHotelsCardSorted(IEnumerable<HotelCard> tourCards, bool isLowStarsSort, bool isHighStarsSort)
        {
            if (isLowStarsSort)
            {
                return tourCards.OrderByDescending(c => c.StarsCount);
            }
            else if (isHighStarsSort)
            {
                return tourCards.OrderBy(c => c.StarsCount);
            }
            else
            {
                return tourCards.OrderBy(c => c.Name);
            }
        }
    }
}