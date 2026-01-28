using FluentAssertions;
using JsonApiSerializer.JsonApi;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Get : UserControllerSpecificationBase
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
            _response = SendJsonApiRequest<UserResource>(HttpMethod.Get, $"/api/v1/tenants/{_tenant.Id}/users/{_user.Id}");
        }

        [Test]
        public void It_Should_Return_Tenant()
        {
            _response.Data.Should().NotBeNull();

            _response.Data.Email.Should().Be(_user.Email);

            _response.Data.FirstName.Should().Be(_user.FirstName);

            _response.Data.LastName.Should().Be(_user.LastName);

            _response.Data.Phone.Should().Be(_user.Phone);

            _response.Data.IdNumber.Should().Be(_user.IdNumber);
        }
    }
}
