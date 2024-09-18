namespace Application.TenantConfigs.Queries.GeTenantConfigs
{
    public record GetTenantConfigsResponse(
        string Name,
        string Value,
        bool Enabled
    );
}
