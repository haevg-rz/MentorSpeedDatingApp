using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isMentor))
            {
                return null;
            }

            return isMentor ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}