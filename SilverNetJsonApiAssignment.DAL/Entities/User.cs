namespace SilverNetJsonApiAssignment.Entities
{
    public class User
    {
        public long Id { get; protected set; }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string Phone { get; protected set; }

        public string Email { get; protected set; }

        public string IdNumber { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public Tenant Tenant { get; protected set; }

        protected User() { }

        public User(string firstName, string lastName, string phone, string email, string idNumber, Tenant tenant)
        {
            this.FirstName = firstName;

            this.LastName = lastName;

            this.Phone = phone;

            this.Email = email;

            this.IdNumber = idNumber;

            this.Tenant = tenant;

            this.CreationDate = DateTime.UtcNow;
        }

        public void SetFirstName(string firstName)
        {
            this.FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            this.LastName = lastName;
        }

        public void SetPhone(string phone)
        {
            this.Phone = phone;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }

        public void SetIdNumber(string idNumber)
        {
            this.IdNumber = idNumber;
        }

        public void SetTenant(Tenant tenant)
        {
            this.Tenant = tenant;
        }
    }

}
