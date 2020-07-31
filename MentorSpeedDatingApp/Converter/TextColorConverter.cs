using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MentorSpeedDatingApp.Converter
{
    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isMentor))
            {
                return null;
            }

            return isMentor
                ? new SolidColorBrush((Color) ColorConverter.ConvertFromString("#FF20B613"))
                : new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}