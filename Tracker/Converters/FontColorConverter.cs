using ControlzEx.Theming;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Tracker
{
    class IsEqualOrGreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            if(double.TryParse(parameter.ToString(), out double result))
            {
                if(result > (double)value)
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Refresh ({((double)value).ToString("#0")})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
