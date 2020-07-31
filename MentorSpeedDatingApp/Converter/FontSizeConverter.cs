using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isMentor))
            {
                return null;
            }

            return isMentor ? 24 : 15;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}