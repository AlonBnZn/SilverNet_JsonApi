using FluentAssertions;
using SilverNetJsonApiAssignment.Entities;

namespace SilverNetJsonApiAssigment.Tests.UserRepositoryTests
{
    public class When_Calling_Create : UserRepositorySpecificationBase
    {
        private User _newUser = null!;

        protected override void Given()
        {
            base.Given();

            _newUser = CreateTestUser("Test", "lastTest", "0542897554", "test@corp.com", "123456789");
        }

        protected override void When()
        {
            Repository.CreateUserAsync(_newUser).Wait();
        }

        [Test]
        public void Should_Be_Persisted_In_Database()
        {
            User user = DbContext.Users.First(x => x.Id.Equals(1));

            user.FirstName.Should().Be("Test");

            user.LastName.Should().Be("lastTest");

            user.Phone.Should().Be("0542897554");

            user.Email.Should().Be("test@corp.com");

            user.IdNumber.Should().Be("123456789");
        }
    }
}
