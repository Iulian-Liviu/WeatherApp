using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;

namespace WeatherApp.Converters
{
    public class DateTimeToNameConverter : ICommunityToolkitValueConverter
    {
        object ICommunityToolkitValueConverter.DefaultConvertReturnValue => throw new NotImplementedException();

        object ICommunityToolkitValueConverter.DefaultConvertBackReturnValue => throw new NotImplementedException();

        Type ICommunityToolkitValueConverter.FromType => throw new NotImplementedException();

        Type ICommunityToolkitValueConverter.ToType => throw new NotImplementedException();

        object ICommunityToolkitValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            string day = dateTime.ToString("dddd, dd.MM");
            return day;
        }

        object ICommunityToolkitValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
