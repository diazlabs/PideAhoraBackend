namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
           Guid UserId,
           string firstName,
           string lastName,
           string email,
           List<string> tenants,
           List<string> permissions,
           List<string> roles);
    }
}
