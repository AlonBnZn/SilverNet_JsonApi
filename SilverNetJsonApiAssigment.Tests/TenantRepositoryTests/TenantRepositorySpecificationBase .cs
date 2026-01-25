using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;

namespace SilverNetJsonApiAssigment.Tests.TenantRepositoryTests
{
    public abstract class TenantRepositorySpecificationBase : SpecificationBase
    {
        protected CommandDbContext DbContext = null!;
        protected TenantRepository Repository = null!;
        private SqliteConnection _connection = null!;

        protected override void Given()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<CommandDbContext>()
                .UseSqlite(_connection)
                .Options;

            DbContext = new CommandDbContext(options);

            DbContext.Database.EnsureCreated();

            Repository = new TenantRepository(DbContext);
        }


        protected override void Cleanup()
        {
            DbContext?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }

        protected Tenant CreateTestTenant(string name = "TestCorp",
                                           string email = "test@corp.com",
                                           string phone = "0542897554")
        {
            return new Tenant(name, email, phone);
        }
    }
}
