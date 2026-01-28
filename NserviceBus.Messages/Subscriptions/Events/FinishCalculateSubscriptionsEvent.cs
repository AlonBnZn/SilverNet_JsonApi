namespace NserviceBus.Messages.Subscriptions.Events
{
    public class FinishCalculateSubscriptionsEvent : IEvent
    {
        public Guid JobGuid { get; set; }
    }
}
