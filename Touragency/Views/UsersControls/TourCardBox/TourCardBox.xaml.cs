using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Touragency.Model;
using Touragency.Views.Windows;

namespace Touragency.Views.UsersControls
{
    public partial class TourCardBox : UserControl
    {
        public TourCardBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        public TourCardBox(TourCard tourCard) : this()
        {
            InitializeTourCard(tourCard);
        }

        private void InitializeTourCard(TourCard tourCard)
        {
            if (tourCard == null)
            {
                tourCard = new TourCard();
            }

            PreviewPicture = tourCard.CardPreview;
            TourName = tourCard.Name;
            TourCity = tourCard.City;
            TourCost = tourCard.Price;
            TicketsCount = tourCard.TicketsCount;
            TourId = tourCard.Id;
            TourDescription = tourCard.Description;
            TourCategories = tourCard.Categories;
            TourIsActual = tourCard.IsActual;

            UpdateTourCard();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TourEditing += TourCardBox_TourEditing;
            TourDeleting += TourCardBox_TourDeleting;
            TourRestoring += TourCardBox_TourRestoring;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            TourEditing -= TourCardBox_TourEditing;
            TourDeleting -= TourCardBox_TourDeleting;
            TourRestoring -= TourCardBox_TourRestoring;
        }

        private void TourCardBox_TourEditing(object sender, RoutedEventArgs e)
        {
            if (!Session.CheckTourUpdatePermission(Permissions.UpdateTour))
            {
                MessageBox.Show("У вас нет прав редактирования карточек туров.","Ошибка обновления тура");
                return;
            }
            var editWindow = new TourCardEditerWnd(_tourCard);
            if (editWindow.ShowDialog() == true)
            {
                UpdateTourCardFromEditWindow(editWindow.TourCard);
                RaiseEvent(new RoutedEventArgs(EditedEvent));
            }
        }

        private void TourCardBox_TourDeleting(object sender, RoutedEventArgs e)
        {
            if (!Session.CheckTourUpdatePermission(Permissions.DeleteTour))
            {
                MessageBox.Show("У вас нет права обновления статуса карточек туров.", "Ошибка удаления тура");
                return;
            }
            TourIsActual = false;
            RaiseEvent(new RoutedEventArgs(DeletedEvent));
        }
        private void TourCardBox_TourRestoring(object sender, RoutedEventArgs e)
        {
            if (!Session.CheckTourUpdatePermission(Permissions.DeleteTour))
            {
                MessageBox.Show("У вас нет права редактирования карточек туров.", "Ошибка обновления тура");
                return;
            }
            TourIsActual = true;
            RaiseEvent(new RoutedEventArgs(RestoredEvent));
        }

        private void UpdateTourCardFromEditWindow(TourCard updatedTourCard)
        {
            PreviewPicture = updatedTourCard.CardPreview;
            TourName = updatedTourCard.Name;
            TourCity = updatedTourCard.City;
            TourCost = updatedTourCard.Price;
            TicketsCount = updatedTourCard.TicketsCount;
            TourDescription = updatedTourCard.Description;
            TourCategories = updatedTourCard.Categories;
            TourIsActual = updatedTourCard.IsActual;
            TourId = updatedTourCard.Id;
        }

        private void UpdateTourCard()
        {
            _tourCard.Id = TourId;
            _tourCard.CardPreview = PreviewPicture;
            _tourCard.Name = TourName;
            _tourCard.City = TourCity;
            _tourCard.Price = TourCost;
            _tourCard.TicketsCount = TicketsCount;
            _tourCard.Description = TourDescription;
            _tourCard.Categories = TourCategories;
            _tourCard.IsActual = TourIsActual;
        }

        public void ShowEditControls()
        {
            EditControlsColumn.Width = new GridLength(48);
            if (TourIsActual)
            {
                ControlsButtonActualTourSpl.Visibility = Visibility.Visible;
                ControlsButtonNonactualTourSpl.Visibility = Visibility.Collapsed;
            }
            else
            {
                ControlsButtonActualTourSpl.Visibility = Visibility.Collapsed;
                ControlsButtonNonactualTourSpl.Visibility = Visibility.Visible;
            }
        }

        public void HideEditControls()
        {
            EditControlsColumn.Width = new GridLength(0);
        }

        private void CopyTourDescriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TourDescriptionTbx != null && !string.IsNullOrEmpty(TourDescriptionTbx.Text))
            {
                Clipboard.SetText(TourDescriptionTbx.Text);
            }
        }

        private void ToggleDescriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FullDescriptionBorder.Visibility == Visibility.Visible)
            {
                FullDescriptionBorder.Visibility = Visibility.Collapsed;
                ToggleDescriptionBtn.Content = "Показать описание";
            }
            else
            {
                FullDescriptionBorder.Visibility = Visibility.Visible;
                ToggleDescriptionBtn.Content = "Скрыть описание";
            }
        }

        public ImageSource PreviewImage
        {
            get => TourPreviewImg.Source;
            set => TourPreviewImg.Source = value;
        }
    }
}