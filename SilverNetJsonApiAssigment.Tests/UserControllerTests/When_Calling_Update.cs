using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Update : UserControllerSpecificationBase
    {
        private Request<UserResource> _request = null!;

        private DocumentRoot<UserResource> _response = null!;

        private Tenant _tenant = null!;

        private User _user = null!;

        protected override void Given()
        {
            base.Given();

            _tenant = new Tenant("Test", "test@test.com", "0555555555");

            DbContext.Tenants.Add(_tenant);

            _user = new User("Test", "lastTEst", "0555555555", "test@test.com", "123456789", _tenant);

            DbContext.Users.Add(_user);

            DbContext.SaveChanges();

            _request = new Request<UserResource>
            {
                Data = new JsonApiData<UserResource>
                {
                    Type = "users",
                    Id = _user.Id.ToString(),
                    Attributes = new UserResource
                    {
                        FirstName = "Test2",
                    }
                }
            };
        }

        protected override void When()
        {
            _response = SendJsonApiRequest<UserResource>(HttpMethod.Patch, $"/api/v1/tenants/{_tenant.Id}/users/{_user.Id}", _request);
        }

        [Test]
        public void It_Should_Update_User()
        {
            User user = DbContext.Users.First(x => x.Id.Equals(_user.Id));

            DbContext.Entry(user).Reload();

            user.Should().NotBeNull();

            user.FirstName.Should().Be(_request.Data.Attributes.FirstName);
        }
    }
}
