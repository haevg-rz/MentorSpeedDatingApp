using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class PersonToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (value == null)
                {
                    return "";
                }
            }

            return String.IsNullOrEmpty((String) values[0])
                ? values[1] + ", " + values[2]
                : values[0] + " " + values[1] + ", " + values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}