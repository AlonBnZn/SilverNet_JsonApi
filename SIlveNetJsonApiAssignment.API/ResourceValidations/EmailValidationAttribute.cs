using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SilveNetJsonApiAssignment.Service.ResourceValidations
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor))!;

            var request = httpContextAccessor!.HttpContext!.Request;

            string? email = value as string;

            if (request.Method == HttpMethods.Post)
            {

                if (string.IsNullOrWhiteSpace(email))
                {
                    return new ValidationResult("Email is required.");
                }

                if (!EmailRegex.IsMatch(email))
                {
                    return new ValidationResult("Invalid email format.");
                }
            }
            else if (request.Method == HttpMethods.Patch)
            {

                if (email is not null && !EmailRegex.IsMatch(email))
                {
                    return new ValidationResult("Invalid email format.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

