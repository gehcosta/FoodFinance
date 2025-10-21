using System.Text.RegularExpressions;

namespace FoodFinance.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Valida se um valor decimal é válido e positivo
 /// </summary>
      public static bool IsValidDecimal(string value, out decimal result)
        {
if (string.IsNullOrWhiteSpace(value))
            {
      result = 0;
        return false;
  }

  if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, 
     System.Globalization.CultureInfo.InvariantCulture, out result))
   {
   return result > 0;
            }

            return false;
}

    /// <summary>
        /// Valida se um valor float é válido e positivo
/// </summary>
  public static bool IsValidFloat(string value, out float result)
    {
       if (string.IsNullOrWhiteSpace(value))
   {
       result = 0;
     return false;
   }

if (float.TryParse(value, System.Globalization.NumberStyles.Any, 
  System.Globalization.CultureInfo.InvariantCulture, out result))
    {
  return result > 0;
  }

return false;
   }

        /// <summary>
/// Formata um valor decimal para exibição em moeda brasileira
   /// </summary>
        public static string FormatCurrency(decimal value)
 {
 return value.ToString("C2", new System.Globalization.CultureInfo("pt-BR"));
        }

        /// <summary>
  /// Formata um TimeSpan para exibição
 /// </summary>
    public static string FormatTimeSpan(TimeSpan time)
   {
   return time.ToString(@"hh\:mm\:ss");
   }

        /// <summary>
        /// Valida se uma data é válida (não é futura)
 /// </summary>
        public static bool IsValidDate(DateTime date)
   {
   return date.Date <= DateTime.Now.Date;
   }

   /// <summary>
      /// Limpa caracteres não numéricos mantendo ponto e vírgula
 /// </summary>
   public static string CleanNumericInput(string input)
        {
if (string.IsNullOrEmpty(input)) return string.Empty;
   
    // Remove tudo exceto dígitos, ponto e vírgula
            return Regex.Replace(input, @"[^\d.,]", "");
        }
    }
}
