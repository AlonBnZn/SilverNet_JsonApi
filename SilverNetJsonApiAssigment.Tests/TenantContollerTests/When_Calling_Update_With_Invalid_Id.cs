using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Update_With_Invalid_Id : TenantControllerSpecificationBase
    {
        private Request<TenantResource> _request = null!;

        private DocumentRoot<TenantResource> _response = null!;

        private Tenant _tenant = null!;

        private long _invalidId = 99;
        protected override void Given()
        {
            base.Given();

            _tenant = new Tenant("Test", "test@test.com", "0555555555");

            DbContext.Tenants.Add(_tenant);

            DbContext.SaveChanges();

            _request = new Request<TenantResource>
            {
                Data = new JsonApiData<TenantResource>
                {
                    Type = "tenants",
                    Id = _invalidId.ToString(),
                    Attributes = new TenantResource
                    {
                        Name = "Test2",
                    }
                }
            };
        }

        protected override void When()
        {
            _response = SendJsonApiRequest<TenantResource>(HttpMethod.Patch, $"/api/v1/tenants/{_tenant.Id}", _request);
        }

        [Test]
        public void Should_Not_Update_Tenant()
        {
            Tenant tenant = DbContext.Tenants.First(x => x.Id.Equals(_tenant.Id));

            DbContext.Entry(tenant).Reload();

            tenant.Should().NotBeNull();

            tenant.Name.Should().Be(_tenant.Name);

            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be($"Expected '{_tenant.Id}' instead of '{_invalidId}'.");
        }
    }
}
