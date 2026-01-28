namespace NServiceBus.Service.Entities
{
    public class User
    {
        public long Id { get; protected set; }

        public Tenant Tenant { get; protected set; } = null!;

        public User()
        {

        }

        public User(long id, Tenant tenant)
        {
            Id = id;

            Tenant = tenant;
        }
    }
}
