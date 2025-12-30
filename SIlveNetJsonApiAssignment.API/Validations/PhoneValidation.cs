using System.Text.RegularExpressions;

namespace SilverNetJsonApiAssignment.API.Validations
{
    public static class PhoneValidation
    {
        public static void ValidatePhone(string phone)
        {
            Regex phoneRegex = new Regex(@"^(\+?\d{1,3})?0?\d{8,9}$");

            if (string.IsNullOrWhiteSpace(phone) || !phoneRegex.IsMatch(phone))
            {
                throw new ArgumentException("Invalid phone number format.");
            }
        }
    }
}
