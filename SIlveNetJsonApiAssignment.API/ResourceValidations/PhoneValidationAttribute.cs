using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SilveNetJsonApiAssignment.Service.ResourceValidations
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        private static readonly Regex PhoneRegex = new Regex(@"^(\+?\d{1,3})?0?\d{8,9}$", RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor))!;

            var request = httpContextAccessor!.HttpContext!.Request;

            string? phone = value as string;

            if (request.Method == HttpMethods.Post)
            {

                if (string.IsNullOrWhiteSpace(phone))
                {
                    return new ValidationResult("phone is required.");
                }

                if (!PhoneRegex.IsMatch(phone))
                {
                    return new ValidationResult("Invalid phone format.");
                }
            }
            else if (request.Method == HttpMethods.Patch)
            {

                if (phone is not null && !PhoneRegex.IsMatch(phone))
                {
                    return new ValidationResult("Invalid phone format.");
                }
            }

            return ValidationResult.Success;

        }

    }
}
