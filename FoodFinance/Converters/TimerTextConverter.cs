using System.Globalization;

namespace FoodFinance.Converters
{
    public class TimerTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isRunning)
  {
     return isRunning ? "Pausar" : "Iniciar";
            }
            return "Iniciar";
   }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
   }
    }
}
