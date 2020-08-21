using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MentorSpeedDatingApp.Validators
{
    public class HourValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(this.InputIsValid(value),
                "Bitte eine korrekte Uhrzeit (Stunden) angeben. Zum Beispiel: 12");
        }

        public bool InputIsValid(object value)
        {
            var input = value?.ToString();
            Regex reg = new Regex("^(20|21|22|23|[01]\\d|\\d)$");
            MatchCollection matches = reg.Matches(input);
            return matches.Any();
        }
    }
}