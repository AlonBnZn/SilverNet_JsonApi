namespace SilverNetJsonApiAssignment.Entities
{
    public class Tenant
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public string Phone { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public List<User> Users { get; set; }

        protected Tenant() { }

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
