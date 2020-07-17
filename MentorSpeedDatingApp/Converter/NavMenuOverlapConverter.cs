using System;
using System.Globalization;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace MentorSpeedDatingApp.Converter
{
    public class NavMenuOverlapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RadNavigationView radNav = (RadNavigationView)parameter;
            if (radNav.IsPaneOpen)
            {
                return "Auto";
            }
            return "40";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}