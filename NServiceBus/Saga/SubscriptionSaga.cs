using Microsoft.EntityFrameworkCore;
using NserviceBus.Messages.Subscriptions;
using NserviceBus.Messages.Subscriptions.Commands;
using NserviceBus.Messages.Subscriptions.Events;
using NServiceBus.Service.Data;
using NServiceBus.Service.Entities;

namespace NServiceBus.Service.Saga
{
    public class SubscriptionSaga : Saga<SubsriptionSagaData>,
                                        IAmStartedByMessages<StartCalculateSubscriptionsCommand>,
                                        IHandleMessages<FinishCalculateSubscriptionsEvent>
    {
        private CommandDbContext _commandDbContext;

        public SubscriptionSaga(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }

        public async Task Handle(StartCalculateSubscriptionsCommand message, IMessageHandlerContext context)
        {
            try
            {
                List<Subscription> subscriptions = await _commandDbContext.Subscriptions.Include(x => x.Tenant).ToListAsync(context.CancellationToken);

                Data.TotalMessages = subscriptions.Count;

                if (Data.TotalMessages == 0)
                {
                    MarkAsComplete();
                }

                foreach (var subscription in subscriptions)
                {
                    BillingCycle billingCycle = new BillingCycle(DateTime.Now.Year, DateTime.Now.Month);

                    await context.SendLocal<CalculateSubscriptionsCommand>(m =>
                    {
                        m.JobGuid = message.JobGuid;
                        m.SubscriptionId = subscription.Id;
                        m.TenantId = subscription.Tenant.Id;
                        m.BillingCycle = billingCycle;
                    }).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                MarkAsComplete();
            }

            await Task.CompletedTask;
        }

        public async Task Handle(FinishCalculateSubscriptionsEvent message, IMessageHandlerContext context)
        {
            Data.ProcessedMessages++;

            if (Data.ProcessedMessages >= Data.TotalMessages)
            {
                MarkAsComplete();
            }

            await Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SubsriptionSagaData> mapper)
        {
            mapper.MapSaga(saga => saga.JobGuid)
                  .ToMessage<StartCalculateSubscriptionsCommand>(message => message.JobGuid)
                  .ToMessage<FinishCalculateSubscriptionsEvent>(message => message.JobGuid);
        }
    }
}
