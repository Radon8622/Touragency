using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Touragency.Views.UsersControls
{
    public partial class PriceRangeSelector : UserControl
    {
        // Регистрация событий
        public static readonly RoutedEvent PriceRangeChangedEvent =
            EventManager.RegisterRoutedEvent(
                "PriceRangeChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(PriceRangeSelector));

        public static readonly RoutedEvent PriceRangeEditingFinishedEvent =
            EventManager.RegisterRoutedEvent(
                "PriceRangeEditingFinished",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(PriceRangeSelector));

        // Dependency Properties
        public static readonly DependencyProperty MinPriceProperty =
            DependencyProperty.Register(
                "MinPrice",
                typeof(int),
                typeof(PriceRangeSelector),
                new PropertyMetadata(
                    0,
                    OnPriceChanged,
                    CoercePrice));

        public static readonly DependencyProperty MaxPriceProperty =
            DependencyProperty.Register(
                "MaxPrice",
                typeof(int),
                typeof(PriceRangeSelector),
                new PropertyMetadata(
                    0,
                    OnPriceChanged,
                    CoercePrice));

        // Свойства для настройки шрифта меток
        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.Register(
                "LabelFontFamily",
                typeof(FontFamily),
                typeof(PriceRangeSelector),
                new PropertyMetadata(SystemFonts.MessageFontFamily));

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register(
                "LabelFontSize",
                typeof(double),
                typeof(PriceRangeSelector),
                new PropertyMetadata(SystemFonts.MessageFontSize));

        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.Register(
                "LabelFontWeight",
                typeof(FontWeight),
                typeof(PriceRangeSelector),
                new PropertyMetadata(FontWeights.Normal));

        public static readonly DependencyProperty LabelFontStyleProperty =
            DependencyProperty.Register(
                "LabelFontStyle",
                typeof(FontStyle),
                typeof(PriceRangeSelector),
                new PropertyMetadata(FontStyles.Normal));

        // Свойства для настройки шрифта полей ввода
        public static readonly DependencyProperty InputFontFamilyProperty =
            DependencyProperty.Register(
                "InputFontFamily",
                typeof(FontFamily),
                typeof(PriceRangeSelector),
                new PropertyMetadata(SystemFonts.MessageFontFamily));

        public static readonly DependencyProperty InputFontSizeProperty =
            DependencyProperty.Register(
                "InputFontSize",
                typeof(double),
                typeof(PriceRangeSelector),
                new PropertyMetadata(SystemFonts.MessageFontSize));

        public static readonly DependencyProperty InputFontWeightProperty =
            DependencyProperty.Register(
                "InputFontWeight",
                typeof(FontWeight),
                typeof(PriceRangeSelector),
                new PropertyMetadata(FontWeights.Normal));

        public static readonly DependencyProperty InputFontStyleProperty =
            DependencyProperty.Register(
                "InputFontStyle",
                typeof(FontStyle),
                typeof(PriceRangeSelector),
                new PropertyMetadata(FontStyles.Normal));

        // Конструктор
        public PriceRangeSelector()
        {
            InitializeComponent();
        }

        // События
        public event RoutedEventHandler PriceRangeChanged
        {
            add => AddHandler(PriceRangeChangedEvent, value);
            remove => RemoveHandler(PriceRangeChangedEvent, value);
        }

        public event RoutedEventHandler PriceRangeEditingFinished
        {
            add => AddHandler(PriceRangeEditingFinishedEvent, value);
            remove => RemoveHandler(PriceRangeEditingFinishedEvent, value);
        }

        // Основные свойства диапазона цен
        public int MinPrice
        {
            get => (int)GetValue(MinPriceProperty);
            set => SetValue(MinPriceProperty, value);
        }

        public int MaxPrice
        {
            get => (int)GetValue(MaxPriceProperty);
            set => SetValue(MaxPriceProperty, value);
        }

        // Свойства шрифта меток
        public FontFamily LabelFontFamily
        {
            get => (FontFamily)GetValue(LabelFontFamilyProperty);
            set => SetValue(LabelFontFamilyProperty, value);
        }

        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public FontWeight LabelFontWeight
        {
            get => (FontWeight)GetValue(LabelFontWeightProperty);
            set => SetValue(LabelFontWeightProperty, value);
        }

        public FontStyle LabelFontStyle
        {
            get => (FontStyle)GetValue(LabelFontStyleProperty);
            set => SetValue(LabelFontStyleProperty, value);
        }

        // Свойства шрифта полей ввода
        public FontFamily InputFontFamily
        {
            get => (FontFamily)GetValue(InputFontFamilyProperty);
            set => SetValue(InputFontFamilyProperty, value);
        }

        public double InputFontSize
        {
            get => (double)GetValue(InputFontSizeProperty);
            set => SetValue(InputFontSizeProperty, value);
        }

        public FontWeight InputFontWeight
        {
            get => (FontWeight)GetValue(InputFontWeightProperty);
            set => SetValue(InputFontWeightProperty, value);
        }

        public FontStyle InputFontStyle
        {
            get => (FontStyle)GetValue(InputFontStyleProperty);
            set => SetValue(InputFontStyleProperty, value);
        }

        // Метод коррекции значений цены
        private static object CoercePrice(DependencyObject d, object baseValue)
        {
            int value = (int)baseValue;
            return value < 0 ? 0 : value;
        }

        // Обработчик изменения цены
        private static void OnPriceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PriceRangeSelector;
            if (control == null) return;

            // Синхронизация минимального и максимального значений
            if (e.Property == MinPriceProperty && control.MinPrice > control.MaxPrice)
            {
                control.MaxPrice = control.MinPrice;
            }
            else if (e.Property == MaxPriceProperty && control.MaxPrice < control.MinPrice)
            {
                control.MinPrice = control.MaxPrice;
            }

            // Вызов события изменения диапазона
            control.RaiseEvent(new RoutedEventArgs(PriceRangeChangedEvent));
        }

        // Обработчик ввода текста (валидация)
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        // Обработчик потери фокуса
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PriceRangeEditingFinishedEvent));
        }

        // Методы для вызова событий (можно использовать в наследниках)
        protected virtual void OnPriceRangeChanged(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        protected virtual void OnPriceRangeEditingFinished(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
    }
}