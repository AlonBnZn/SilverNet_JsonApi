namespace SilverNetJsonApiAssignment.API.Validations
{
    public interface ITenantValidation
    {
        void ValidateTenant(string name, string phone, string email);
        void ValidateName(string name);
        void ValidatePhone(string phone);
        void ValidateEmail(string email);


    }
}
