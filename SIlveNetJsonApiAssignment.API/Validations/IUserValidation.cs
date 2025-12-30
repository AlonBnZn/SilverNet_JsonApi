namespace SilverNetJsonApiAssignment.API.Validations
{
    public interface IUserValidation
    {
        void ValidateUser(string firstName, string lastName, string phone, string email, string idNumber);
        void ValidateFirstName(string firstName);
        void ValidateLastName(string lastName);
        void ValidatePhone(string phone);
        void ValidateEmail(string email);
        void ValidateIdNumber(string idNumber);
    }
}
