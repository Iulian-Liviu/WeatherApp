using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;
using WeatherApp.Models;

namespace WeatherApp.Converters
{
    public class WeatherCodeToNameConverter : ICommunityToolkitValueConverter
    {
        public object DefaultConvertReturnValue => throw new NotImplementedException();

        public object DefaultConvertBackReturnValue => throw new NotImplementedException();

        public Type FromType => throw new NotImplementedException();

        public Type ToType => throw new NotImplementedException();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var codes = WeatherCodes.GetValuePairs().Result;
            var code = (long)value;

            if (codes.ContainsKey(code.ToString()))
            {
                string desc;
                if (codes.TryGetValue(code.ToString(), out desc))
                {
                    return desc;
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
