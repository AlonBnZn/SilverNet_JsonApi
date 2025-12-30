using System.Text.RegularExpressions;

namespace SilverNetJsonApiAssignment.API.Validations
{
    public static class EmailValidation
    {
        public static void ValidateEmail(string email)
        {
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
        }
    }
}
