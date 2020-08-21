using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class MatchingMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isHeaderRow))
                return new Thickness(5);

            return isHeaderRow ? new Thickness(5, 0, 5, 10) : new Thickness(5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}