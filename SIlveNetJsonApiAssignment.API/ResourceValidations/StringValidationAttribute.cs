using System.ComponentModel.DataAnnotations;

namespace SilveNetJsonApiAssignment.Service.ResourceValidations
{
    public class StringValidationAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        private readonly string _fieldName;

        public StringValidationAttribute(int maxLength , string fieldName)
        {
            _maxLength = maxLength;

            _fieldName = fieldName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor))!;

            var request = httpContextAccessor!.HttpContext!.Request;

            string? fieldValue = value as string;

            if (request.Method == HttpMethods.Post)
            {
                if (string.IsNullOrWhiteSpace(fieldValue))
                {
                    return new ValidationResult($"{_fieldName} is required.");
                }

                if (fieldValue.Length > _maxLength)
                {
                    return new ValidationResult($"{_fieldName} cannot exceed {_maxLength} characters.");
                }
            }
            else if (request.Method == HttpMethods.Patch)
            {
                if (fieldValue != null && fieldValue.Length > _maxLength)
                {
                    return new ValidationResult($"{_fieldName} cannot exceed {_maxLength} characters.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
