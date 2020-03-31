using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MentorSpeedDatingApp.Validators
{
    public class MinuteValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(ValidateNoValidationResult(value), "Bitte eine korrekte Uhrzeit (Minuten) angeben. Zum Beispiel: 15");
        }

        public bool ValidateNoValidationResult(object value)
        {
            var input = value?.ToString();
            Regex reg = new Regex("^([0-5]\\d)$");
            MatchCollection matches = reg.Matches(input);
            return matches.Any();
        }
    }
}