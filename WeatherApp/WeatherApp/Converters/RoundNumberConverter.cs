using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;

namespace WeatherApp.Converters
{
    public class RoundNumberConverter : ICommunityToolkitValueConverter
    {
        public object DefaultConvertReturnValue => throw new NotImplementedException();

        public object DefaultConvertBackReturnValue => throw new NotImplementedException();

        public Type FromType => throw new NotImplementedException();

        public Type ToType => throw new NotImplementedException();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = Math.Round((double)value);
            if (number.ToString().Contains('-') && number == 0)
            {
                return number.ToString().Replace("-", "");
            }
            return Math.Round(number);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
