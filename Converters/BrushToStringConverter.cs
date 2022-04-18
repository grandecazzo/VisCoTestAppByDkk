using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ViscoTestApp.Converters
{
    class BrushToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Remove(1, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(value as string));
            }
            catch
            {
                return Brushes.Transparent;
            }         
        }
    }
}