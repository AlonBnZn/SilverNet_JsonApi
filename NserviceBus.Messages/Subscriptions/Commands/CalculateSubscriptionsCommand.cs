namespace NserviceBus.Messages.Subscriptions.Commands
{
    public class CalculateSubscriptionsCommand : ICommand
    {
        public Guid JobGuid { get; set; }

        public long SubscriptionId { get; set; }

        public long TenantId { get; set; }

        public BillingCycle BillingCycle { get; set; }
    }
}
