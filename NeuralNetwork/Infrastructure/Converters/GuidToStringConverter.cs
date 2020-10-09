using System;
using System.Globalization;
using System.Windows.Data;

namespace NeuralNetwork.Infrastructure.Converters
{
    public class GuidToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Guid.Parse(value.ToString());
        }
    }
}