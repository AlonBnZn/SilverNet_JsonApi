namespace NServiceBus.Service.Entities
{
    public class Tenant
    {
        public long Id { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public Tenant()
        {

        }

        public Tenant(long id)
        {
            Id = id;
        }
    }
}
