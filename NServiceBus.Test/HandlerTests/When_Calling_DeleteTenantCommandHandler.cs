using FluentAssertions;
using NserviceBus.Messages.Tenants.Commands;
using NServiceBus.Service.Entities;
using NServiceBus.Service.Handlers;
using NServiceBus.Testing;

namespace NServiceBus.Test.HandlerTests
{
    public class When_Calling_DeleteTenantCommandHandler : NServiceBusSpecificationBase
    {
        private TestableMessageHandlerContext _context = null!;

        private DeleteTenantCommand _deleteTenantCommand = null!;

        private DeleteTenantCommandHandler _deleteTenantCommandHandler = null!;

        private long _tenantId = 1;

        protected override void Given()
        {
            base.Given();

            _deleteTenantCommand = new DeleteTenantCommand() { TenantId = _tenantId };

            _context = new TestableMessageHandlerContext();

            _deleteTenantCommandHandler = new DeleteTenantCommandHandler(DbContext);

            Tenant tenant = new Tenant(_tenantId);

            DbContext.Tenants.Add(tenant);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            base.When();

            _deleteTenantCommandHandler.Handle(_deleteTenantCommand, _context);
        }

        [Test]
        public void It_It_Should_Delete_Tenant()
        {
            Tenant? tenant = DbContext.Tenants.FirstOrDefault(x => x.Id.Equals(_tenantId));

            tenant.Should().BeNull();
        }
    }
}
