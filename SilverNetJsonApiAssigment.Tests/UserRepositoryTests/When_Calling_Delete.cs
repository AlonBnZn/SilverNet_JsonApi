using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SilverNetJsonApiAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverNetJsonApiAssigment.Tests.UserRepositoryTests
{
    public class When_Calling_Delete : UserRepositorySpecificationBase
    {
        private User _existingUser = null!;

        protected override void Given()
        {
            base.Given();

            _existingUser = CreateTestUser();

            DbContext.Users.Add(_existingUser);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            Repository.DeleteUserAsync(_existingUser.Id).Wait();
        }

        [Test]
        public void Should_Be_Deleted()
        {
            User? user = DbContext.Users.FirstOrDefault(x => x.Id.Equals(_existingUser.Id));

            user.Should().BeNull();
        }
    }
}
