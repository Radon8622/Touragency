using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Touragency.Views.UsersControls
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isFilled = (bool)value;
            var brush = parameter as Brush ?? Brushes.Gold;
            return isFilled ? brush : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
    public class StarViewModel
    {
        public bool IsFilled { get; set; }
    }
}
