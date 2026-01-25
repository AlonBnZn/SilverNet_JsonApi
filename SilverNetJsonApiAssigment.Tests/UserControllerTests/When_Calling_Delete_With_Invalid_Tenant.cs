using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Delete_With_Invalid_Tenant : UserControllerSpecificationBase
    {
        private User _user = null!;

        private Tenant _tenant = null!;

        private DocumentRoot<UserResource> _response = null!;

        protected override void Given()
        {
            base.Given();

            _tenant = new Tenant("Test", "test@test.com", "0555555555");

            DbContext.Tenants.Add(_tenant);

            _user = new User("Test", "lastTEst", "0555555555", "test@test.com", "123456789", _tenant);

            DbContext.Users.Add(_user);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            _response = SendJsonApiRequest<UserResource>(HttpMethod.Delete, $"/api/v1/tenants/{99}/users/{_user.Id}");
        }

        [Test]
        public void Should_Not_Be_Deleted()
        {
            User? user = DbContext.Users.FirstOrDefault(x => x.Id.Equals(_user.Id));

            user.Should().NotBeNull();
        }

        [Test]
        public void Should_Return_UnproccessableEntity()
        {
            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be("Error Deleting user");

        }
    }
}
