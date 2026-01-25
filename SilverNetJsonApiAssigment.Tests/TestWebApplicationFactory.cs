using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Entities;
using SilverNetJsonApiAssignment.Service.Repositories;

namespace SilverNetJsonApiAssigment.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private SqliteConnection _connection = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            builder.ConfigureServices(services =>
            {
                var commandDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CommandDbContext>));

                services.Remove(commandDbContext!);

                services.AddDbContext<CommandDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                var queryDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<QueryDbContext>));

                services.Remove(queryDbContext!);

                services.AddDbContext<QueryDbContext>(options =>
                {
                    options.UseSqlite(_connection);
                });

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                    dbContext.Database.EnsureCreated();
                }

                Mock<IAuthorizationHandler> authorizationHandlerMock = CreateAuthorizationHandlerMock();

                var authorizationHandlerService = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(IAuthorizationHandler));

                services.Remove(authorizationHandlerService!);

                services.AddSingleton(authorizationHandlerMock.Object);
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Close();
                _connection?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Mocks
        private static Mock<IAuthorizationHandler> CreateAuthorizationHandlerMock()
        {
            Mock<IAuthorizationHandler> authorizationHandlerMock = new Mock<IAuthorizationHandler>();

            authorizationHandlerMock.Setup(x => x.HandleAsync(It.IsAny<AuthorizationHandlerContext>()))
                                    .Returns((AuthorizationHandlerContext context) =>
                                    {
                                        var pendingRequirements = context.PendingRequirements.ToList();

                                        foreach (var requirement in pendingRequirements)
                                        {
                                            context.Succeed(requirement);
                                        }

                                        return Task.CompletedTask;
                                    });
            return authorizationHandlerMock;
        }

        private static Mock<ITenantRepository> CreateTenantRepositoryMock()
        {
            Mock<ITenantRepository> tenantRepositoryMock = new Mock<ITenantRepository>();

            tenantRepositoryMock.Setup(x => x.CreateTenantAsync(It.IsAny<Tenant>()));

            tenantRepositoryMock.Setup(x => x.DeleteTenantAsync(It.IsAny<long>()));

            return tenantRepositoryMock;
        }
        #endregion
    }
}