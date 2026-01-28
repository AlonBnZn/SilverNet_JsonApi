namespace NserviceBus.Messages.Users.Event
{
    public class UserCreatedEvent : IEvent
    {
        public long UserId { get; set; }

        public long TenantId { get; set; }
    }
}
