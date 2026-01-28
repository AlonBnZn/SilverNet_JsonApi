namespace NserviceBus.Messages.Tenants.Commands
{
    public class CreateTenantCommand : ICommand
    {
        public long TenantId { get; set; }
    }
}
