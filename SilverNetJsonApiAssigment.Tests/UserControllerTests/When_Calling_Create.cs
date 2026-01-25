using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Create : UserControllerSpecificationBase
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
                        LastName = "lastTest",
                        Email = "test@test.com",
                        Phone = "0555555555",
                        IdNumber = "123456789",
                    }
                }
            };

            _response = SendJsonApiRequest<UserResource>(HttpMethod.Post, $"/api/v1/tenants/{_tenant.Id}/users", _request);
        }

        [Test]
        public void Should_Return_User()
        {
            _response.Data.Should().NotBeNull();

            _response.Data.FirstName.Should().Be(_request.Data.Attributes.FirstName);

            _response.Data.LastName.Should().Be(_request.Data.Attributes.LastName);

            _response.Data.Email.Should().Be(_request.Data.Attributes.Email);

            _response.Data.Phone.Should().Be(_request.Data.Attributes.Phone);

            _response.Data.IdNumber.Should().Be(_request.Data.Attributes.IdNumber);
        }

        [Test]
        public void Should_Persist_User()
        {
            User user = DbContext.Users.First(x => x.Id.Equals(_response.Data.Id));

            user.Should().NotBeNull();

            user.FirstName.Should().Be(_request.Data.Attributes.FirstName);

            user.LastName.Should().Be(_request.Data.Attributes.LastName);

            user.Email.Should().Be(_request.Data.Attributes.Email);

            user.Phone.Should().Be(_request.Data.Attributes.Phone);

            user.IdNumber.Should().Be(_request.Data.Attributes.IdNumber);
        }
    }
}
