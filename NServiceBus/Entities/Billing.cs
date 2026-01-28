
using NserviceBus.Messages.Subscriptions;

namespace NServiceBus.Service.Entities
{
    public class Billing
    {
        public long Id { get; protected set; }

        public Subscription Subscription { get; set; }

        public BillingCycle BillingCycle { get; protected set; } = null!;

        public decimal Amount { get; protected set; }

        public Billing()
        {

        }

        public Billing(BillingCycle billingCycle, decimal amount, Subscription subscription)
        {
            Subscription = subscription;

            BillingCycle = billingCycle;

            Amount = amount;
        }
    }
}
