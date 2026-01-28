using Microsoft.EntityFrameworkCore;
using NserviceBus.Messages.Subscriptions.Commands;
using NserviceBus.Messages.Subscriptions.Events;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Handlers
{
    public class CalculateSubscriptionsCommandHandler : IHandleMessages<CalculateSubscriptionsCommand>
    {
        private CommandDbContext _commandDbContext;

        public CalculateSubscriptionsCommandHandler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public async Task Handle(CalculateSubscriptionsCommand message, IMessageHandlerContext context)
        {
            Subscription? subscription = await _commandDbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id.Equals(message.SubscriptionId), context.CancellationToken);

            if (subscription is null)
            {
                throw new Exception("Subscription doesnt exist");
            }

            Billing billing = new Billing(message.BillingCycle, subscription.Amount, subscription);

            await _commandDbContext.Billings.AddAsync(billing, context.CancellationToken);

            await _commandDbContext.SaveChangesAsync(context.CancellationToken);

            await context.Publish<FinishCalculateSubscriptionsEvent>(m =>
            {
                m.JobGuid = message.JobGuid;
            }).ConfigureAwait(false);

            await Task.CompletedTask;
        }
    }
}
