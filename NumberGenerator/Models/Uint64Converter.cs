using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NumberGenerator.Models
{
    internal class Uint64Converter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue; 
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (UInt64.TryParse(value?.ToString(), out ulong result))
                return result;
            else return null;
        }
    }
}
