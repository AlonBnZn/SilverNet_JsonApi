using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Create_With_Invalid_LastName : UserControllerSpecificationBase
    {
        private Request<UserResource> _request = null!;

        private DocumentRoot<UserResource> _response = null!;

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
            _request = new Request<UserResource>
            {
                Data = new JsonApiData<UserResource>
                {
                    Type = "users",
                    Attributes = new UserResource
                    {
                        FirstName = "test",
                        Email = "test@test.com",
                        Phone = "0555555555",
                        IdNumber = "123456789"
                    }
                }
            };

            _response = SendJsonApiRequest<UserResource>(HttpMethod.Post, $"/api/v1/tenants/{_tenant.Id}/users", _request);
        }

        [Test]
        public void Should_Return_UnproccessableEntity()
        {
            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be("The LastName field is required.");
        }
    }
}
