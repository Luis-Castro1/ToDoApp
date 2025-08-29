using System.Globalization;

namespace ToDoApp.Mobile.Converters
{
    public class NullOrStringToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Devuelve true si hay texto (o sea, si hay error)
            return !string.IsNullOrWhiteSpace(value?.ToString());
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
