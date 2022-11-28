using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace WeatherApp.Converters
{
    public class StringToDateToNameConverter : ICommunityToolkitValueConverter
    {
        public object DefaultConvertReturnValue => throw new NotImplementedException();

        public object DefaultConvertBackReturnValue => throw new NotImplementedException();

        public Type FromType => throw new NotImplementedException();

        public Type ToType => throw new NotImplementedException();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;
            DateTime dateTime = DateTime.Parse(date);
            string day = dateTime.ToString("dddd dd.MM");
            return day;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
