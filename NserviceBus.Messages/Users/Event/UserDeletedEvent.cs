namespace NserviceBus.Messages.Users.Event
{
    public class UserDeletedEvent : IEvent
    {
        public long UserId { get; set; }

        public long TenantId { get; set; }
    }
}
