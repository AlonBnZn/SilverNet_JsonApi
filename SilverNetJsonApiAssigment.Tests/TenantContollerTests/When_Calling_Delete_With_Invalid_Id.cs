using FluentAssertions;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Delete_With_Invalid_Id : TenantControllerSpecificationBase
    {
        private Tenant _tenant = null!;

        protected override void Given()
        {
            base.Given();

            _tenant = new Tenant("Test", "test@test.com", "0555555555");

            DbContext.Tenants.Add(_tenant);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            SendJsonApiRequest<TenantResource>(HttpMethod.Delete, $"/api/v1/tenants/{99}");
        }

        [Test]
        public void Should_Not_Be_Deleted()
        {
            Tenant? tenant = DbContext.Tenants.FirstOrDefault(x => x.Id.Equals(_tenant.Id));

            tenant.Should().NotBeNull();
        }
    }
}
