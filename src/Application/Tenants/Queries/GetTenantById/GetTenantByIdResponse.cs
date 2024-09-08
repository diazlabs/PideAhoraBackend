namespace Application.Tenants.Queries.GetTenantById
{
    public record GetTenantByIdResponse(
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
