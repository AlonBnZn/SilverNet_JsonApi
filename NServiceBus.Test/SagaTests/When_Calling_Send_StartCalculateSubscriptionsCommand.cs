using FluentAssertions;
using NserviceBus.Messages.Subscriptions.Commands;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Saga;
using NServiceBus.Testing;

namespace NServiceBus.Test.SagaTests
{
    public class When_Calling_Send_StartCalculateSubscriptionsCommand : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _testableMessageHandlerContext = null!;

        private TestableMessageSession _testableMessageSession = null!;

        private SubscriptionSaga _subscriptionSaga = null!;

        private StartCalculateSubscriptionsCommand _command = null!;

        private long _subscriptionId;

        private Guid _jobGuid = Guid.NewGuid();

        protected override void Given()
        {
            base.Given();

            _testableMessageHandlerContext = new TestableMessageHandlerContext();

            _testableMessageSession = new TestableMessageSession();

            Tenant tenant = new Tenant()
            {
                Id = 1
            };

            DbContext.Tenants.Add(tenant);

            Service.Entities.Subscription subscription = new Service.Entities.Subscription(5, tenant);

            DbContext.Subscriptions.Add(subscription);

            DbContext.SaveChanges();

            _subscriptionId = subscription.Id;

            _subscriptionSaga = new SubscriptionSaga(DbContext);

            _subscriptionSaga.Data = new SubsriptionSagaData();

            _command = new StartCalculateSubscriptionsCommand() { JobGuid = _jobGuid };
        }

        protected override async void When()
        {
            base.When();

            await _subscriptionSaga.Handle(_command, _testableMessageHandlerContext);
        }

        [Test]
        public void It_It_Should_Send_CalculateSubscriptionCommand()
        {
            var sentMessage = _testableMessageHandlerContext.SentMessages.First().Message;

            sentMessage.Should().BeOfType<CalculateSubscriptionsCommand>();

            ((CalculateSubscriptionsCommand)sentMessage).TenantId.Should().Be(1);

            ((CalculateSubscriptionsCommand)sentMessage).SubscriptionId.Should().Be(_subscriptionId);

            ((CalculateSubscriptionsCommand)sentMessage).JobGuid.Should().Be(_jobGuid);
        }

        [Test]
        public void It_It_Should_Update_SagaData()
        {
            _subscriptionSaga.Data.TotalMessages.Should().Be(1);

            _subscriptionSaga.Data.ProcessedMessages.Should().Be(0);
        }
    }
}
