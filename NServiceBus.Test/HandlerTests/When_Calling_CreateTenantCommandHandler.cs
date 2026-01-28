using FluentAssertions;
using NserviceBus.Messages.Tenants.Commands;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Handlers;
using NServiceBus.Testing;

namespace NServiceBus.Test.HandlerTests
{
    public class When_Calling_CreateTenantCommandHandler : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _context = null!;

        private CreateTenantCommand _createTenantCommand = null!;

        private CreateTenantCommandHandler _createTenantCommandHandler = null!;

        private long _tenantId = 1;

        protected override void Given()
        {
            base.Given();

            _createTenantCommand = new CreateTenantCommand() { TenantId = _tenantId };

            _context = new TestableMessageHandlerContext();

            _createTenantCommandHandler = new CreateTenantCommandHandler(DbContext);
        }

        protected override void When()
        {
            base.When();

            _createTenantCommandHandler.Handle(_createTenantCommand, _context);
        }

        [Test]
        public void It_It_Should_Create_Tenant()
        {
            Tenant? tenant = DbContext.Tenants.FirstOrDefault(x => x.Id.Equals(_tenantId));

            tenant.Should().NotBeNull();
        }
    }
}
