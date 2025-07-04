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
    public class ToursViewModel : INotifyPropertyChanged
    {
        private readonly object _lock = new object();
        private CancellationTokenSource _searchCts;
        private ObservableCollection<TourCard> _observableAllTourCards;

        public ObservableCollection<TourCard> TourCards
        {
            get
            {
                if (_observableAllTourCards is null)
                {
                    _observableAllTourCards = TourCardsManager.AllTourCards != null
                        ? new ObservableCollection<TourCard>(TourCardsManager.AllTourCards)
                        : new ObservableCollection<TourCard>();
                }
                TourCardsManager.AllTourCards = _observableAllTourCards;
                return _observableAllTourCards;
            }
            set
            {
                TourCardsManager.AllTourCards = value;
                if (_observableAllTourCards == null)
                {
                    _observableAllTourCards = new ObservableCollection<TourCard>(value);
                }
                else
                {
                    var added = value.Except(_observableAllTourCards).ToList();
                    foreach (var tourCard in added)
                    {
                        _observableAllTourCards.Add(tourCard);
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

        public async Task LoadTourCardsAsync(IEnumerable<TourCardBox> cardBoxes)
        {
            try
            {
                var loadedCards = await Task.Run(() =>
                {
                    var tempCollection = new List<TourCard>();
                    foreach (var cardBox in cardBoxes)
                    {
                        var card = new TourCard(cardBox);
                        if (!tempCollection.Any(t => t.Id == card.Id))
                            tempCollection.Add(card);
                    }
                    return tempCollection;
                });

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    lock (_lock)
                    {
                        TourCards.Clear();
                        foreach (var card in loadedCards)
                            TourCards.Add(card);
                    }
                });
            }
            catch (TaskCanceledException) { }
        }

        public async Task LoadTourCardsAsync()
        {
            List<TourCardBox> cardBoxes;
            lock (_lock)
            {
                cardBoxes = TourCards.Select(t => new TourCardBox(t)).ToList();
            }
            await LoadTourCardsAsync(cardBoxes);
        }

        public List<string> GetDistinctCategories()
        {
            return TourCards
                .Where(t => t.Categories != null)
                .SelectMany(t => t.Categories)
                .Select(category => category.Value)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        public async Task<IEnumerable<TourCard>> GetFilteredTourCardsAsync(
            string searchText,
            bool isRelevantSort,
            bool isCheaperSort,
            bool isActualFilter,
            bool isNonactualFilter,
            uint minPrice,
            uint maxPrice,
            List<string> selectedCategories)
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
                    return GetToursAppliedFiltersAndSearch(
                        TourCards,
                        lowerSearchText,
                        isRelevantSort,
                        isCheaperSort,
                        isActualFilter,
                        isNonactualFilter,
                        minPrice,
                        maxPrice,
                        selectedCategories);
                }, _searchCts.Token);
            }
            catch (TaskCanceledException) { return Enumerable.Empty<TourCard>(); }
            catch (ObjectDisposedException) { return Enumerable.Empty<TourCard>(); }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return Enumerable.Empty<TourCard>();
            }
        }

        public void UpdateTourCard(TourCardBox cardBox)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_lock)
                {
                    var restoredCard = TourCards.FirstOrDefault(t => t.Id == cardBox.TourId);
                    if (restoredCard != null)
                    {
                        var index = TourCards.IndexOf(restoredCard);
                        TourCards.Remove(restoredCard);
                        TourCards.Insert(index, new TourCard(cardBox));
                    }
                }
            });
        }

        public void EditTourCard(TourCardBox editedCardBox)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (_lock)
                {
                    var editedCard = TourCards.FirstOrDefault(t => t.Id == editedCardBox.TourId);
                    if (editedCard != null)
                    {
                        TourCards.Remove(editedCard);
                        TourCards.Insert(0, new TourCard(editedCardBox));
                    }
                }
            });
        }

        public void AddTourCard(TourCard tourCard)
        {
            if (!TourCards.Any(t => t.Id == tourCard.Id))
            {
                TourCards.Add(tourCard);
            }
        }

        private IEnumerable<TourCard> GetToursAppliedFiltersAndSearch(
            IEnumerable<TourCard> tourCards,
            string searchText,
            bool isRelevantSort,
            bool isCheaperSort,
            bool isActualFilter,
            bool isNonactualFilter,
            uint minPrice,
            uint maxPrice,
            List<string> selectedCategories)
        {
            var filtered = GetToursCardMeetsSearchCriteria(tourCards, searchText);

            if (selectedCategories.Any())
            {
                filtered = filtered.Where(t =>
                    t.Categories != null &&
                    selectedCategories.All(s =>
                        t.Categories.Select(c => c.Value).Contains(s)));
            }

            filtered = GetToursCardActualFilterApplied(filtered, isActualFilter, isNonactualFilter);
            filtered = TourRecommender.GetRecommendedTours(filtered);
            filtered = GetToursCardMeetsPriceRangeCriteria(filtered, minPrice, maxPrice);
            return GetToursCardSorted(filtered, isRelevantSort, isCheaperSort);
        }

        private IEnumerable<TourCard> GetToursCardMeetsSearchCriteria(
            IEnumerable<TourCard> tourCards, string searchText)
        {
            return tourCards.Where(t =>
                t.Name.ToLowerInvariant().Contains(searchText) ||
                t.City.ToLowerInvariant().Contains(searchText) ||
                t.Categories.Any(c => c.Value.ToLower().Contains(searchText))||
                TourHotelCardsJuncter.GetHotelCardsRelatedWithTour(t).Any(c => c.Name.ToLowerInvariant().Contains(searchText)));
        }

        private IEnumerable<TourCard> GetToursCardMeetsPriceRangeCriteria(
            IEnumerable<TourCard> tourCards, uint minPrice, uint maxPrice)
        {
            return tourCards.Where(t =>
                t.Price >= minPrice &&
                t.Price <= maxPrice);
        }

        private IEnumerable<TourCard> GetToursCardSorted(IEnumerable<TourCard> tourCards, bool isRelevantSort, bool isCheaperSort)
        {
            if (isRelevantSort)
            {
                return tourCards.OrderBy(t => (t.Categories != null && t.Categories.Any()) ? 0 : 1);
            }
            else if (isCheaperSort)
            {
                return tourCards.OrderBy(t => t.Price)
                                .ThenBy(t => (t.Categories != null && t.Categories.Any()) ? 0 : 1);
            }
            else
            {
                return tourCards.OrderByDescending(t => t.Price)
                                .ThenBy(t => (t.Categories != null && t.Categories.Any()) ? 0 : 1);
            }
        }

        private IEnumerable<TourCard> GetToursCardActualFilterApplied(
            IEnumerable<TourCard> tourCards, bool isActualFilter, bool isNonactualFilter)
        {
            if (isActualFilter)
            {
                return tourCards.Where(t => t.IsActual);
            }
            else if (isNonactualFilter)
            {
                return tourCards.Where(t => !t.IsActual);
            }
            return tourCards;
        }
    }
}
