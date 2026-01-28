namespace NserviceBus.Messages.Subscriptions.Commands
{
    public class StartCalculateSubscriptionsCommand : ICommand
    {
        public Guid JobGuid { get; set; }
    }
}
