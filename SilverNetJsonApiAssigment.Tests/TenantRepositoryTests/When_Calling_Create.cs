using FluentAssertions;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantRepositoryTests
{
    public class When_Calling_Create : TenantRepositorySpecificationBase
    {
        private Tenant _newTenant = null!;

        protected override void Given()
        {
            base.Given();

            _newTenant = CreateTestTenant("NewCorp", "new@corp.com", "0501234567");
        }

        protected override void When()
        {
            Repository.CreateTenantAsync(_newTenant).Wait();
        }

        [Test]
        public void It_Should_Be_Persisted_In_Database()
        {
            Tenant tenant = DbContext.Tenants.First(x => x.Id.Equals(1));

            tenant.Name.Should().Be("NewCorp");

            tenant.Email.Should().Be("new@corp.com");

            tenant.Phone.Should().Be("0501234567");
        }
    }
}