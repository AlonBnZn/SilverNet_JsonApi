namespace SilveNetJsonApiAssignment.Service.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync();
        Task<string?> LoginAsync(long tenantId, long? userId);
    }
}
