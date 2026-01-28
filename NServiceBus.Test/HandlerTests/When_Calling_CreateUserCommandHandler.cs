using FluentAssertions;
using NserviceBus.Messages.Users.Event;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Handlers;
using NServiceBus.Testing;

namespace NServiceBus.Test.HandlerTests
{
    public class When_Calling_CreateUserCommandHandler : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _context = null!;

        private UserCreatedEvent _createUserCommand = null!;

        private CreateUserEventHandler _createUserCommandHandler = null!;

        private long _tenantId = 1;

        private long _userId = 1;

        protected override void Given()
        {
            base.Given();

            Tenant tenant = new Tenant(_tenantId);

            DbContext.Tenants.Add(tenant);

            DbContext.SaveChanges();

            _createUserCommand = new UserCreatedEvent() { TenantId = _tenantId, UserId = _userId };

            _context = new TestableMessageHandlerContext();

            _createUserCommandHandler = new CreateUserEventHandler(DbContext);
        }

        protected override void When()
        {
            base.When();

            _createUserCommandHandler.Handle(_createUserCommand, _context);
        }

        [Test]
        public void It_It_Should_Create_User()
        {
            User? user = DbContext.Users.FirstOrDefault(x => x.Id.Equals(_userId));

            user.Should().NotBeNull();
        }
    }
}
