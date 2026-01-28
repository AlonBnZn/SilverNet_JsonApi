using NserviceBus.Messages.Tenants.Commands;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Handlers
{
    public class DeleteTenantCommandHandler : IHandleMessages<DeleteTenantCommand>
    {
        private CommandDbContext _commandDbContext;

        public DeleteTenantCommandHandler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public Task Handle(DeleteTenantCommand message, IMessageHandlerContext context)
        {
            Tenant? existingTenant = _commandDbContext.Tenants.FirstOrDefault(t => t.Id.Equals(message.TenantId));

            if (existingTenant != null)
            {
                _commandDbContext.Tenants.Remove(existingTenant);

                _commandDbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
