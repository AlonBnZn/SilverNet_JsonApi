using System.Text.Json.Serialization;

namespace SilverNetJsonApiAssignment.DAL.Entities
{
    public class User
    {
        public long Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string IdNumber { get; private set; }

        public DateTime CreationDate { get; private set; }

        [JsonIgnore]
        public Tenant Tenant { get; private set; }

        public User(string firstName, string lastName, string phone, string email, string idNumber)
        {
            this.FirstName = firstName;

            this.LastName = lastName;

            this.Phone = phone;

            this.Email = email;

            this.IdNumber = idNumber;

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
