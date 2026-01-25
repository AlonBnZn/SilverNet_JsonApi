using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverNetJsonApiAssigment.Tests.UserRepositoryTests
{
    public abstract class UserRepositorySpecificationBase : SpecificationBase
    {
        protected CommandDbContext DbContext = null!;
        protected UserRepository Repository = null!;
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

            Repository = new UserRepository(DbContext);
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

        protected User CreateTestUser(string firstName = "Test",string lastName = "lastTest",
                                           string phone = "0542897554",
                                           string email = "test@corp.com",
                                           string idNumber = "123456789",
                                           Tenant? tenant = null
                                           )
        {
            if (tenant is null)
            {
                tenant = CreateTestTenant();
            }
            return new User(firstName,lastName, phone, email,idNumber , tenant);
        }
    }
}
