using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_List : TenantControllerSpecificationBase
    {
        private DocumentRoot<List<TenantResource>> _response = null!;

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
            _response = SendJsonApiRequest<List<TenantResource>>(HttpMethod.Get, $"/api/v1/tenants");
        }

        [Test]
        public void It_Should_Return_List()
        {
            _response.Data.Should().NotBeNull();

            _response.Data.Should().HaveCount(1);
        }
    }
}
