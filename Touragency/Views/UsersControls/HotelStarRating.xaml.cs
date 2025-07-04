using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Touragency.Views.UsersControls
{
    public partial class HotelStarRating : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty StarCountProperty =
            DependencyProperty.Register(nameof(StarCount), typeof(int), typeof(HotelStarRating),
                new PropertyMetadata(0, OnStarCountChanged));

        public int StarCount
        {
            get => (int)GetValue(StarCountProperty);
            set => SetValue(StarCountProperty, value);
        }

        public static readonly DependencyProperty StarColorProperty =
            DependencyProperty.Register(nameof(StarColor), typeof(Brush), typeof(HotelStarRating),
                new PropertyMetadata(Brushes.Gold, OnStarColorChanged));

        public Brush StarColor
        {
            get => (Brush)GetValue(StarColorProperty);
            set => SetValue(StarColorProperty, value);
        }

        private static void OnStarCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HotelStarRating control)
            {
                control.UpdateStars();
            }
        }

        private static void OnStarColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HotelStarRating control)
            {
                control.Resources["StarColorResource"] = control.StarColor;
            }
        }

        private List<StarViewModel> _stars = new List<StarViewModel>();
        public List<StarViewModel> Stars
        {
            get => _stars;
            set
            {
                _stars = value;
                OnPropertyChanged(nameof(Stars));
            }
        }

        public HotelStarRating()
        {
            InitializeComponent();
            DataContext = this;
            UpdateStars();
        }

        private void UpdateStars()
        {
            var list = new List<StarViewModel>();
            for (int i = 0; i < 5; i++)
                list.Add(new StarViewModel { IsFilled = i < StarCount });
            Stars = list;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
