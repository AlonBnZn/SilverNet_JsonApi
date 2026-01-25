using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;

namespace SilverNetJsonApiAssigment.Tests.TenantContollerTests
{
    public class When_Calling_Create_With_Invalid_Phone : TenantControllerSpecificationBase
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
                        Email = "test@test.com"
                    }
                }
            };

            _response = SendJsonApiRequest<TenantResource>(HttpMethod.Post, "/api/v1/tenants", _request);
        }

        [Test]
        public void Should_Return_UnproccessableEntity()
        {
            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be("The Phone field is required.");
        }
    }
}
