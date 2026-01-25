using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Create : TenantControllerSpecificationBase
    {
        private Request<TenantResource> _request = null!;

        private DocumentRoot<TenantResource> _response = null!;

        protected override void Given()
        {
            base.Given();
        }

        protected override void When()
        {
            _request = new Request<TenantResource>
            {
                Data = new JsonApiData<TenantResource>
                {
                    Type = "tenants",
                    Attributes = new TenantResource
                    {
                        Name = "test",
                        Email = "test@test.com",
                        Phone = "0555555555"
                    }
                }
            };

            _response = SendJsonApiRequest<TenantResource>(HttpMethod.Post, "/api/v1/tenants", _request);
        }

        [Test]
        public void Should_Return_Tenant()
        {
            _response.Data.Should().NotBeNull();

            _response.Data.Email.Should().Be(_request.Data.Attributes.Email);

            _response.Data.Name.Should().Be(_request.Data.Attributes.Name);

            _response.Data.Phone.Should().Be(_request.Data.Attributes.Phone);
        }

        [Test]
        public void Should_Persist_Tenant()
        {
            Tenant tenant = DbContext.Tenants.First(x => x.Id.Equals(_response.Data.Id));

            tenant.Should().NotBeNull();

            tenant.Email.Should().Be(_request.Data.Attributes.Email);

            tenant.Name.Should().Be(_request.Data.Attributes.Name);

            tenant.Phone.Should().Be(_request.Data.Attributes.Phone);
        }
    }
}
