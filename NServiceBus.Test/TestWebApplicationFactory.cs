using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Service.Data;

namespace NServiceBus.Test
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

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                    dbContext.Database.EnsureCreated();
                }
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

    }
}
