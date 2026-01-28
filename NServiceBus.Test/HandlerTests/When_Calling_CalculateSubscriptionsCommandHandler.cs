using FluentAssertions;
using NserviceBus.Messages.Subscriptions.Commands;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Handlers;
using NServiceBus.Testing;

namespace NServiceBus.Test.HandlerTests
{
    public class When_Calling_CalculateSubscriptionsCommandHandler : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _context = null!;

        private CalculateSubscriptionsCommand _calculateSubscriptionsCommand = null!;

        private CalculateSubscriptionsCommandHandler _calculateSubscriptionsCommandHandler = null!;

        private long _tenantId = 1;

        private long _subscriptionId;

        protected override void Given()
        {
            base.Given();

            Tenant tenant = new Tenant()
            {
                Id = 1
            };

            DbContext.Tenants.Add(tenant);

            Service.Entities.Subscription subscription = new Service.Entities.Subscription(5, tenant);

            DbContext.Subscriptions.Add(subscription);

            DbContext.SaveChanges();

            _subscriptionId = subscription.Id;

            _calculateSubscriptionsCommand = new CalculateSubscriptionsCommand()
            {
                JobGuid = Guid.NewGuid(),
                TenantId = _tenantId,
                SubscriptionId = _subscriptionId,
                BillingCycle = new NserviceBus.Messages.Subscriptions.BillingCycle() { Month = 1, Year = 2026 }
            };

            _context = new TestableMessageHandlerContext();

            _calculateSubscriptionsCommandHandler = new CalculateSubscriptionsCommandHandler(DbContext);
        }

        protected override async void When()
        {
            base.When();

            await _calculateSubscriptionsCommandHandler.Handle(_calculateSubscriptionsCommand, _context);
        }

        [Test]
        public void It_It_Should_Create_Billing()
        {
            Billing? billing = DbContext.Billings.FirstOrDefault(x => x.Subscription.Id.Equals(_subscriptionId));

            billing.Should().NotBeNull();
        }
    }
}
