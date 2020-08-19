using System;
using System.Globalization;
using System.Windows.Controls;

namespace MentorSpeedDatingApp.Validators
{
    public class UserInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is String userInput))
                return new ValidationResult(false, "Ungültige Eingabe!");

            return userInput.Length >= 2
                ? new ValidationResult(true, null)
                : new ValidationResult(false, "Name und Vorname müssen eine Mindestlänge größer 2 haben.");
        }
    }
}