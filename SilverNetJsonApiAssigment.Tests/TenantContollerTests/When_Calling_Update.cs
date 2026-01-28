using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Update : TenantControllerSpecificationBase
    {
        private Request<TenantResource> _request = null!;

        private DocumentRoot<TenantResource> _response = null!;

        private Tenant _tenant = null!;

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
                    Id = _tenant.Id.ToString(),
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
        public void It_Should_Update_Tenant()
        {
            Tenant tenant = DbContext.Tenants.First(x => x.Id.Equals(_tenant.Id));

            DbContext.Entry(tenant).Reload();

            tenant.Should().NotBeNull();

            tenant.Name.Should().Be(_request.Data.Attributes.Name);
        }
    }
}
