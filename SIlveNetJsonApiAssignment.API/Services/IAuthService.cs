namespace SilverNetJsonApiAssignment.API.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(long tenantId, long? userId);
    }
}
