using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.FamilyShow.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var conv = new BoolToVisibilityConverter();
            return conv.Convert(!(bool)value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var conv = new BoolToVisibilityConverter();
            return !(bool)conv.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
