using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class NavigationMenuVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isVisible))
                return 33;

            return isVisible ? 191 : 33;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}