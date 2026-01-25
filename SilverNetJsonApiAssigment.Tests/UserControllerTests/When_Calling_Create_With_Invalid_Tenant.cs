using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Create_With_Invalid_Tenant : UserControllerSpecificationBase
    {
        private Request<UserResource> _request = null!;

        private DocumentRoot<UserResource> _response = null!;


        protected override void Given()
        {
            base.Given();
        }

        protected override void When()
        {
            _request = new Request<UserResource>
            {
                Data = new JsonApiData<UserResource>
                {
                    Type = "users",
                    Attributes = new UserResource
                    {
                        FirstName = "test",
                        LastName = "lastTest",
                        Email = "test@test.com",
                        Phone = "0555555555",
                        IdNumber = "123456789"
                    }
                }
            };

            _response = SendJsonApiRequest<UserResource>(HttpMethod.Post, $"/api/v1/tenants/{1}/users", _request);
        }

        [Test]
        public void Should_Return_UnproccessableEntity()
        {
            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be("Error creating user");
        }
    }
}
