using Microsoft.IdentityModel.Tokens;
using SilverNetJsonApiAssignment.DAL.Entities;
using SilverNetJsonApiAssignment.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SilverNetJsonApiAssignment.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;

        private readonly IUserRepository _userRepository;

        private readonly ITenantRepository _tenantRepository;

        private readonly IConfiguration _configuration;


        public AuthService(ILogger<AuthService> logger, IUserRepository userRepository, ITenantRepository tenantRepository, IConfiguration configuration)
        {
            this._logger = logger;

            this._userRepository = userRepository;

            this._tenantRepository = tenantRepository;

            this._configuration = configuration;
        }

        public async Task<string?> LoginAsync(long tenantId, long? userId)
        {
            if (userId is not null)
            {
                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(tenantId);

                if (tenant is null)
                {
                    _logger.LogError("Tenant with id {TenantId} not found", tenantId);
                    throw new Exception($"Tenant with id {tenantId} not found");
                }

                User? user = await _userRepository.GetUserByIdAsync(userId!.Value);

                if (user is null)
                {
                    _logger.LogError("User not found");

                    throw new Exception("User not found");
                }
            }
            else
            {
                Tenant? tenant = await _tenantRepository.GetTenantByIdAsync(tenantId);
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
