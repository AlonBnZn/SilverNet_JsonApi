using NserviceBus.Messages.Tenants.Commands;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Handlers
{
    public class CreateTenantCommandHandler : IHandleMessages<CreateTenantCommand>
    {
        private CommandDbContext _commandContext;

        public CreateTenantCommandHandler(CommandDbContext commandDbContext)
        {
            _commandContext = commandDbContext;
        }

        public Task Handle(CreateTenantCommand message, IMessageHandlerContext context)
        {
            _commandContext.Add(new Tenant(message.TenantId));

            _commandContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
