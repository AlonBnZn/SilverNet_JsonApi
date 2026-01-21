using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SilveNetJsonApiAssignment.Service.ResourceValidations
{
    public class IdNumberValidationAttribute : ValidationAttribute
    {
        private static readonly Regex IdNumberRegex = new Regex(@"^\d{1,9}$", RegexOptions.Compiled);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor))!;

            var request = httpContextAccessor!.HttpContext!.Request;

            string? idNumber = value as string;

            if (request.Method == HttpMethods.Post)
            {

                if (string.IsNullOrWhiteSpace(idNumber))
                {
                    return new ValidationResult("IdNumber is required.");
                }

                if (!IdNumberRegex.IsMatch(idNumber))
                {
                    return new ValidationResult("Invalid idNumber format.");
                }
            }
            else if (request.Method == HttpMethods.Patch)
            {

                if (idNumber is not null && !IdNumberRegex.IsMatch(idNumber))
                {
                    return new ValidationResult("Invalid idNumber format.");
                }
            }

            return ValidationResult.Success;

        }

    }
}
