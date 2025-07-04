using System.Windows;
using System.Windows.Controls;

namespace Touragency.Views.UsersControls
{
    public partial class TourCardBox : UserControl
    {
        public static readonly RoutedEvent DeletingEvent = EventManager.RegisterRoutedEvent(
            "TourDeleting",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));
        public static readonly RoutedEvent DeletedEvent = EventManager.RegisterRoutedEvent(
            "TourDeleted",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));

        public static readonly RoutedEvent EditingEvent = EventManager.RegisterRoutedEvent(
            "TourEditing",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));

        public static readonly RoutedEvent EditedEvent = EventManager.RegisterRoutedEvent(
            "TourEdited",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));

        public static readonly RoutedEvent RestoringEvent = EventManager.RegisterRoutedEvent(
            "TourRestoring",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));
        public static readonly RoutedEvent RestoredEvent = EventManager.RegisterRoutedEvent(
            "TourRestored",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));

        public static readonly RoutedEvent SearchHotelsButtonClickEvent = EventManager.RegisterRoutedEvent(
            "SearchHotelsButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TourCardBox));

        public event RoutedEventHandler TourDeleting
        {
            add => AddHandler(DeletingEvent, value);
            remove => RemoveHandler(DeletingEvent, value);
        }
        public event RoutedEventHandler TourDeleted
        {
            add => AddHandler(DeletedEvent, value);
            remove => RemoveHandler(DeletedEvent, value);
        }

        public event RoutedEventHandler TourEditing
        {
            add => AddHandler(EditingEvent, value);
            remove => RemoveHandler(EditingEvent, value);
        }
        public event RoutedEventHandler TourEdited
        {
            add => AddHandler(EditedEvent, value);
            remove => RemoveHandler(EditedEvent, value);
        }

        public event RoutedEventHandler TourRestoring
        {
            add => AddHandler(RestoringEvent, value);
            remove => RemoveHandler(RestoringEvent, value);
        }
        public event RoutedEventHandler TourRestored
        {
            add => AddHandler(RestoredEvent, value);
            remove => RemoveHandler(RestoredEvent, value);
        }
        public event RoutedEventHandler SearchHotelsButtonClick
        {
            add => AddHandler(SearchHotelsButtonClickEvent, value);
            remove => RemoveHandler(SearchHotelsButtonClickEvent, value);
        }

        private void EditTourBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(EditingEvent));
        }

        private void DeleteTourBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(DeletingEvent));
        }

        private void RestoreTourBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(RestoringEvent));
        }
        private void SearchHotelsBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(SearchHotelsButtonClickEvent));
        }
    }
}