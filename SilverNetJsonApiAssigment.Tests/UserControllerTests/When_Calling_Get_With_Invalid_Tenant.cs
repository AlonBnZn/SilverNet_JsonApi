using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Get_With_Invalid_Tenant : UserControllerSpecificationBase
    {
        private DocumentRoot<UserResource> _response = null!;

        private User _user = null!;

        private Tenant _tenant = null!;

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
            _response = SendJsonApiRequest<UserResource>(HttpMethod.Get, $"/api/v1/tenants/{99}/users/{_user.Id}");
        }

        [Test]
        public void It_Should_Return_UnproccessableEntity()
        {
            _response.Data.Should().BeNull();

            _response.Errors.Should().NotBeNull();

            _response.Errors[0].Detail.Should().Be($"Resource of type 'users' with ID '{_user.Id}' does not exist.");

        }
    }
}
