using System;
using System.Globalization;
using System.Windows.Data;

namespace MentorSpeedDatingApp.Converter
{
    public class TimeOfDayMinutesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "0" => "00",
                "1" => "01",
                "2" => "02",
                "3" => "03",
                "4" => "04",
                "5" => "05",
                "6" => "06",
                "7" => "07",
                "8" => "08",
                "9" => "09",
                _ => value
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "00" => new TimeSpan(0, 0, 0),
                "01" => new TimeSpan(0, 1, 0),
                "02" => new TimeSpan(0, 2, 0),
                "03" => new TimeSpan(0, 3, 0),
                "04" => new TimeSpan(0, 4, 0),
                "05" => new TimeSpan(0, 5, 0),
                "06" => new TimeSpan(0, 6, 0),
                "07" => new TimeSpan(0, 7, 0),
                "08" => new TimeSpan(0, 8, 0),
                "09" => new TimeSpan(0, 9, 0),
                _ => value
            };
        }
    }
}