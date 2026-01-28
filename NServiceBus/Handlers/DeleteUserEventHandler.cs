using NserviceBus.Messages.Users.Event;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Handlers
{
    public class DeleteUserEventHandler : IHandleMessages<UserDeletedEvent>
    {
        private CommandDbContext _commandDbContext;

        public DeleteUserEventHandler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public Task Handle(UserDeletedEvent message, IMessageHandlerContext context)
        {
            User? existingUser = _commandDbContext.Users.FirstOrDefault(u => u.Id.Equals(message.UserId));

            if (existingUser != null)
            {
                _commandDbContext.Users.Remove(existingUser);

                _commandDbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
