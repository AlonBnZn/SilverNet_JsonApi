namespace SilverNetJsonApiAssignment.API.Validations
{
    public class TenantValidation : ITenantValidation
    {
        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
            {
                throw new ArgumentException("Name must be at least 2 characters long.");
            }
        }

        public void ValidatePhone(string phone)
        {
            PhoneValidation.ValidatePhone(phone);
        }

        public void ValidateEmail(string email)
        {
            EmailValidation.ValidateEmail(email);
        }
        public void ValidateTenant(string name, string phone, string email)
        {
            ValidateName(name);
            ValidatePhone(phone);
            ValidateEmail(email);
        }
    }
}
