using FluentAssertions;
using NserviceBus.Messages.Users.Event;
using SilveNetJsonApiAssignment.Service.Resources;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserControllerTests
{
    public class When_Calling_Delete : UserControllerSpecificationBase
    {
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
            SendJsonApiRequest<UserResource>(HttpMethod.Delete, $"/api/v1/tenants/{_tenant.Id}/users/{_user.Id}");
        }

        [Test]
        public void It_Should_Be_Deleted()
        {
            User? user = DbContext.Users.FirstOrDefault(x => x.Id.Equals(_user.Id));

            user.Should().BeNull();
        }

        [Test]
        public void It_It_Should_Publish_UserDeletedEvent()
        {
            var sentMessage = TestableMessageSession.PublishedMessages.First().Message;

            ((UserDeletedEvent)sentMessage).UserId.Should().Be(_user.Id);

            ((UserDeletedEvent)sentMessage).TenantId.Should().Be(_tenant.Id);
        }
    }
}
