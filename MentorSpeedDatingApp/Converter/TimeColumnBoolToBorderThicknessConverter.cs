using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class TimeColumnBoolToBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isTimeColumn))
                return new Thickness(0, 0, 0, 0);

            return isTimeColumn ? new Thickness(0, 0, 4, 0) : new Thickness(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}