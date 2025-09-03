using System.Globalization;

namespace ToDoApp.Mobile.Converters
{
    public class InverterBooleanCoverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is bool boolean ? !boolean : false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is bool boolean ? !boolean : false;
        }
    }
}
