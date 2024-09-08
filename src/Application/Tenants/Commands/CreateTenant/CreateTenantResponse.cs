
namespace Application.Tenants.Commands.CreateTenant
{
    public record CreateTenantResponse(
        Guid TenantId,
        Guid UserId,
        string Path,
        string Name,
        string PageTitle,
        string Description,
        string Logo,
        string Category
    );
}
