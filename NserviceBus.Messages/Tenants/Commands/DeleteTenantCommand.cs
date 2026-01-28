namespace NserviceBus.Messages.Tenants.Commands
{
    public class DeleteTenantCommand : ICommand
    {
        public long TenantId { get; set; }
    }
}
