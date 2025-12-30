namespace SilverNetJsonApiAssignment.DAL.Entities
{
    public class Tenant
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public DateTime CreationDate { get; private set; }

        public List<User> Users { get; set; }

        private Tenant() { }

        public Tenant(string name, string email, string phone)
        {
            this.Name = name;

            this.Email = email;

            this.Phone = phone;

            this.CreationDate = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }

        public void SetPhone(string phone)
        {
            this.Phone = phone;
        }
    }
}
