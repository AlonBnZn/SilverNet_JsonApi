using FluentAssertions;
using NserviceBus.Messages.Users.Event;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Handlers;
using NServiceBus.Testing;

namespace NServiceBus.Test.HandlerTests
{
    public class When_Calling_DeleteUserCommandHandler : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _context = null!;

        private UserDeletedEvent _deleteUserCommand = null!;

        private DeleteUserEventHandler _deleteUserCommandHandler = null!;

        private long _tenantId = 1;

        private long _userId = 1;

        protected override void Given()
        {
            base.Given();

            _deleteUserCommand = new UserDeletedEvent() { TenantId = _tenantId, UserId = _userId };

            _context = new TestableMessageHandlerContext();

            _deleteUserCommandHandler = new DeleteUserEventHandler(DbContext);

            Tenant tenant = new Tenant(_tenantId);

            DbContext.Tenants.Add(tenant);

            User user = new User(_userId, tenant);

            DbContext.Users.Add(user);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            base.When();

            _deleteUserCommandHandler.Handle(_deleteUserCommand, _context);
        }

        [Test]
        public void It_It_Should_Delete_User()
        {
            User? user = DbContext.Users.FirstOrDefault(x => x.Id.Equals(_userId));

            user.Should().BeNull();
        }
    }
}
