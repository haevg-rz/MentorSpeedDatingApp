using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class MultiBindingNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string value1 = (string) values[0];
            string value2 = (string) values[1];
            string name;
            if (String.IsNullOrEmpty(value1) || String.IsNullOrWhiteSpace(value1))
            {
                name = value2;
            }
            else if (String.IsNullOrEmpty(value2) || String.IsNullOrWhiteSpace(value2))
            {
                name = value1;
            }
            else
            {
                name = values[0].ToString() + " " + values[1].ToString();
            }

            return name;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string) value).Split(' ');
            return splitValues;
        }
    }
}