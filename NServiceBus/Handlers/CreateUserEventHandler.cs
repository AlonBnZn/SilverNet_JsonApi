using NserviceBus.Messages.Users.Event;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Handlers
{
    public class CreateUserEventHandler : IHandleMessages<UserCreatedEvent>
    {
        private CommandDbContext _commandContext;

        public CreateUserEventHandler(CommandDbContext commandDbContext)
        {
            _commandContext = commandDbContext;
        }

        public Task Handle(UserCreatedEvent message, IMessageHandlerContext context)
        {
            Tenant tenant = _commandContext.Tenants.First(t => t.Id.Equals(message.TenantId));

            _commandContext.Add(new User(message.UserId, tenant));

            _commandContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
