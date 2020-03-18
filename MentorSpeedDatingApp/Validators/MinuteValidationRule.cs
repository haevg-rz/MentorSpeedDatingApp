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
            var input = value?.ToString();
            Regex reg = new Regex("^([0-5]\\d)$");
            MatchCollection matches = reg.Matches(input);
            return new ValidationResult(matches.Any(), "Bitte eine korrekte Uhrzeit (Minuten) angeben. Zum Beispiel: 15");
        }
    }
}