using MentorSpeedDatingApp.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class NoGoDateToFormattedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ValueTuple<Mentor, Mentee> noGoDate))
                return "";

            return noGoDate.Item1.ToString().Trim() + " - " + noGoDate.Item2.ToString().Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}