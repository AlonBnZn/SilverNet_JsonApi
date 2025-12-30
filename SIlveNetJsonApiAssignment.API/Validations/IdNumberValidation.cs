using System.Text.RegularExpressions;

namespace SilverNetJsonApiAssignment.API.Validations
{
    public static class IdNumberValidation
    {
        public static void ValidateIdNumber(string idNumber)
        {
            Regex idNumberRegex = new Regex(@"^\d{1,9}$");

            if (string.IsNullOrWhiteSpace(idNumber) || !idNumberRegex.IsMatch(idNumber))
            {
                throw new ArgumentException("Invalid IdNumber format.");
            }
        }
    }
}
