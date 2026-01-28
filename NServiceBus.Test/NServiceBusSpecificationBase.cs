using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Service.Data;
using NServiceBus.Testing;

namespace NServiceBus.Test
{
    public class NServiceBusSpecificationBase : SpecificationBase
    {
        protected TestWebApplicationFactory Factory = null!;

        protected IConfiguration Configuration = null!;

        protected CommandDbContext DbContext = null!;

        protected TestableMessageSession TestableMessageSession = null!;

        protected override void Given()
        {
            Factory = new TestWebApplicationFactory();

            IServiceScope scope = Factory.Services.CreateScope();

            DbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

            TestableMessageSession = new TestableMessageSession();
        }
        protected override void Cleanup()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}