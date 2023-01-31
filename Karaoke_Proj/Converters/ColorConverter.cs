using System;
using System.Globalization;
using Xamarin.Forms;

/// <summary>
///  This class is realize a IValueConverter for convert string hex to object Color.
/// </summary>

namespace Karaoke_Proj.Converters
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string)value;
            return Color.FromHex(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
