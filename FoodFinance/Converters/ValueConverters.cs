using System.Globalization;

namespace FoodFinance.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
         if (value is decimal decimalValue)
  {
      return decimalValue.ToString("F2", CultureInfo.InvariantCulture);
}
  return string.Empty;
   }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
     {
  if (value is string stringValue && decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
     return result;
          }
      return 0m;
        }
    }

  public class FloatToStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
   if (value is float floatValue)
            {
     return floatValue.ToString("F2", CultureInfo.InvariantCulture);
      }
         return string.Empty;
 }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
      {
   if (value is string stringValue && float.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
     return result;
            }
       return 0f;
        }
  }
}
