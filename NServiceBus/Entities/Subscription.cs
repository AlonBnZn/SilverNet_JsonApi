namespace NServiceBus.Service.Entities
{
    public class Subscription
    {
        public long Id { get; protected set; }

        public decimal Amount { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public Tenant Tenant { get; protected set; } = null!;

        public Subscription()
        {

        }

        public Subscription(decimal amount, Tenant tenant)
        {
            Amount = amount;

            Tenant = tenant;

            CreationDate = DateTime.Now;
        }
    }
}
