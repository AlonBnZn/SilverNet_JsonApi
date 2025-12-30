namespace SilverNetJsonApiAssignment.API.Validations
{
    public class UserValidation : IUserValidation
    {
        public void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2)
            {
                throw new ArgumentException("First name must be at least 2 characters long.");
            }
        }

        public void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2)
            {
                throw new ArgumentException("Last name must be at least 2 characters long.");
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

        public void ValidateIdNumber(string idNumber)
        {
            IdNumberValidation.ValidateIdNumber(idNumber);
        }

        public void ValidateUser(string firstName, string lastName, string phone, string email, string idNumber)
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidatePhone(phone);
            ValidateEmail(email);
            ValidateIdNumber(idNumber);
        }
    }
}
