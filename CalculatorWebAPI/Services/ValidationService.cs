using Calculator.Interfaces;
using System.Text.RegularExpressions;

namespace Calculator.Services
{
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Validate the expression of user input
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string ValidateExpression(string expression)
        {
            if (Regex.IsMatch(expression, @"[^0-9\.+\-\*/()\s]"))
            {
                return "Validation Error: The math expressions consist of unsupported characters.";
            }

            return string.Empty;
        }
    }
}
