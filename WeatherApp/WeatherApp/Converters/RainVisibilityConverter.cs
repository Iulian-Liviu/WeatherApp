using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;

namespace WeatherApp.Converters
{
    public class RainVisibilityConverter : ICommunityToolkitValueConverter
    {
        public object DefaultConvertReturnValue => throw new NotImplementedException();

        public object DefaultConvertBackReturnValue => throw new NotImplementedException();

        public Type FromType => throw new NotImplementedException();

        public Type ToType => throw new NotImplementedException();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rain = (double)value;

            return rain > 0 ? true : (object)false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
