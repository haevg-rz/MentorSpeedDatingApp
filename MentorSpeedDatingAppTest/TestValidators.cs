using System;
using System.Globalization;
using System.Windows.Controls;
using MentorSpeedDatingApp.Validators;
using Xunit;

namespace MentorSpeedDatingAppTest
{
    public class TestValidators
    {
        #region Setup

        private readonly HourValidationRule hvr = new HourValidationRule();
        private readonly MinuteValidationRule mvr = new MinuteValidationRule();
        private readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture;

        #endregion

        #region TestCaseAttributes
        [Theory]
        [InlineData("0", true)]
        [InlineData("6", true)]
        [InlineData("00", true)]
        [InlineData("02", true)]
        [InlineData("10", true)]
        [InlineData("23", true)]
        [InlineData("25", false)]
        [InlineData("235", false)]
        [InlineData("010", false)]
        [InlineData("001000", false)]
        #endregion
        public void Test_HourValidationRule_StringInputs(string input, bool isValid)
        {
            #region Act

            var inputValidation = this.hvr.InputIsValid(input);

            #endregion

            #region Assert

            Assert.Equal(inputValidation, isValid);

            #endregion
        }

        #region TestCaseAttributes
        [Theory]
        [InlineData("0", true)]
        [InlineData("6", true)]
        [InlineData("00", true)]
        [InlineData("02", true)]
        [InlineData("10", true)]
        [InlineData("27", true)]
        [InlineData("59", true)]
        [InlineData("60", false)]
        [InlineData("010", false)]
        [InlineData("001000", false)]
        #endregion
        public void Test_MinuteValidationRule_StringInputs(string input, bool isValid)
        {
            #region Act

            var inputValidation = this.mvr.InputIsValid(input);

            #endregion

            #region Assert

            Assert.Equal(inputValidation, isValid);

            #endregion
        }

        #region TestCaseAttributes
        [Theory]
        [InlineData(0, true)]
        [InlineData(6, true)]
        [InlineData(02, true)]
        [InlineData(10, true)]
        [InlineData(23, true)]
        [InlineData(25, false)]
        [InlineData(235, false)]
        [InlineData(001000, false)]
        #endregion
        public void Test_HourValidationRule_OtherTypeInputs(object input, bool isValid)
        {
            #region Act

            var inputValidation = this.hvr.InputIsValid(input);

            #endregion

            #region Assert

            Assert.Equal(inputValidation, isValid);

            #endregion
        }        
        
        #region TestCaseAttributes
        [Theory]
        [InlineData(0, true)]
        [InlineData(6, true)]
        [InlineData(02, true)]
        [InlineData(10, true)]
        [InlineData(27, true)]
        [InlineData(59, true)]
        [InlineData(60, false)]
        [InlineData(001000, false)]
        #endregion
        public void Test_MinuteValidationRule_OtherTypeInputs(object input, bool isValid)
        {
            #region Act

            var inputValidation = this.mvr.InputIsValid(input);

            #endregion

            #region Assert

            Assert.Equal(inputValidation, isValid);

            #endregion
        }

        #region TestCaseAttributes
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        #endregion
        public void Test_HourValidationRuleValidate_ReturnsValidationRule(bool isValid)
        {
            #region Act

            var validationResult = this.hvr.Validate(isValid, this.cultureInfo);

            #endregion

            #region Assert

            Assert.IsType<ValidationResult>(validationResult);

            #endregion
        }

        #region TestCaseAttributes
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        #endregion
        public void Test_MinuteValidationRuleValidate_ReturnsValidationRule(bool isValid)
        {
            #region Act

            var validationResult = this.mvr.Validate(isValid, this.cultureInfo);

            #endregion

            #region Assert

            Assert.IsType<ValidationResult>(validationResult);

            #endregion
        }
    }
}