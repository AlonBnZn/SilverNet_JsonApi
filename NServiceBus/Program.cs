using Microsoft.EntityFrameworkCore;
using NserviceBus.Messages.Tenants.Commands;
using NServiceBus.Service.Data;
using Serilog;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<CommandDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Host.UseNServiceBus(context =>
        {
            var endpointConfiguration = new EndpointConfiguration("NServiceBus.Service");
            endpointConfiguration.UseSerialization<SystemJsonSerializer>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(CreateTenantCommand), "NServiceBus.Service");
            routing.RouteToEndpoint(typeof(DeleteTenantCommand), "NServiceBus.Service");

            //routing.RegisterPublisher(typeof(CreateUserEvent), "NServiceBus.Service");
            //routing.RegisterPublisher(typeof(DeleteUserEvent), "NServiceBus.Service");

            return endpointConfiguration;
        });

        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}