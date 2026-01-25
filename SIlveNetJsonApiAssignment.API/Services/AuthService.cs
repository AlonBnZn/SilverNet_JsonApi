using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SilveNetJsonApiAssignment.Service.Data;
using SilverNetJsonApiAssignment.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SilveNetJsonApiAssignment.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;

        private readonly IConfiguration _configuration;

        private CommandDbContext _dbContext;

        public AuthService(ILogger<AuthService> logger, IConfiguration configuration, CommandDbContext dbContext)
        {
            _logger = logger;

            _configuration = configuration;

            _dbContext = dbContext;
        }

        public async Task<string> RegisterAsync()
        {
            return GenerateJwtToken(0, 0);
        }

        public async Task<string?> LoginAsync(long tenantId, long? userId)
        {
            if (userId is not null)
            {
                Tenant? tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

                if (tenant is null)
                {
                    _logger.LogError("Tenant with id {TenantId} not found", tenantId);

                    throw new Exception($"Tenant with id {tenantId} not found");
                }

                User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

                if (user is null)
                {
                    _logger.LogError("User not found");

                    throw new Exception("User not found");
                }
            }
            else
            {
                Tenant? tenant = await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

                if (tenant is null)
                {
                    _logger.LogError("Tenant with id {TenantId} not found", tenantId);

                    throw new Exception($"Tenant with id {tenantId} not found");
                }
            }

            return GenerateJwtToken(tenantId, userId);
        }

        private string GenerateJwtToken(long tenantId, long? userId)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key not configured");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("TenantId", tenantId.ToString()),
            };

            if (userId is not null)
            {
                claims.Add(new Claim("UserId", userId!.Value.ToString()));
            }

            var roles = DetermineUserRoles(userId);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpirationHours"] ?? "24")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<string> DetermineUserRoles(long? userId)
        {
            var roles = new List<string>();

            roles.Add("Tenant");

            if (userId is not null)
            {
                roles.Add("User");
            }

            return roles;
        }
    }
}
