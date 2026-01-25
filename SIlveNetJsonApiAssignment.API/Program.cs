using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SilveNetJsonApiAssignment.Service.Authorization;
using SilveNetJsonApiAssignment.Service.Data;
using SilveNetJsonApiAssignment.Service.Definitions;
using SilveNetJsonApiAssignment.Service.Services;
using SilverNetJsonApiAssignment.Data;
using SilverNetJsonApiAssignment.Service.Repositories;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<SilverNetJsonApiAssignmentContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<CommandDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<QueryDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddJsonApi<QueryDbContext>(options =>
        {
            options.UseRelativeLinks = true;
            options.IncludeTotalResourceCount = true;
        });

        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<ITenantRepository, TenantRepository>();

        builder.Services.AddResourceService<UserResourceService>();

        builder.Services.AddResourceService<TenantResourceService>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddResourceDefinition<UserResourceDeinition>();

        builder.Services.AddControllers();

        builder.Services.AddScoped<IAuthService, AuthService>();

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        var secretKey = jwtSettings["SecretKey"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.User, policy =>
                policy.Requirements.Add(new Requirements.UserRequirement()));

            options.AddPolicy(Policies.Tenant, policy =>
                policy.Requirements.Add(new Requirements.TenantRequirement()));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();

        var app = builder.Build();

        app.UseRouting();

        app.UseJsonApi();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}