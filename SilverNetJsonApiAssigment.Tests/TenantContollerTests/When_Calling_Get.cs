using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Get : TenantControllerSpecificationBase
    {
        private DocumentRoot<TenantResource> _response = null!;

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
            _response = SendJsonApiRequest<TenantResource>(HttpMethod.Get, $"/api/v1/tenants/{_tenant.Id}");
        }

        [Test]
        public void It_Should_Return_Tenant()
        {
            _response.Data.Should().NotBeNull();

            _response.Data.Email.Should().Be(_tenant.Email);

            _response.Data.Name.Should().Be(_tenant.Name);

            _response.Data.Phone.Should().Be(_tenant.Phone);
        }
    }
}
