using FluentAssertions;
using SilverNetJsonApiAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverNetJsonApiAssigment.Tests.TenantRepositoryTests
{
    public class When_Calling_Delete : TenantRepositorySpecificationBase
    {
        private Tenant _existingTenant = null!;

        protected override void Given()
        {
            base.Given();

            _existingTenant = CreateTestTenant("NewCorp", "new@corp.com", "0501234567");

            DbContext.Tenants.Add(_existingTenant);

            DbContext.SaveChanges();
        }

        protected override void When()
        {
            Repository.DeleteTenantAsync(_existingTenant.Id).Wait();
        }

        [Test]
        public void Should_Be_Deleted()
        {
            Tenant? tenant = DbContext.Tenants.FirstOrDefault(x => x.Id.Equals(_existingTenant.Id));

            tenant.Should().BeNull();
        }

    }
}
