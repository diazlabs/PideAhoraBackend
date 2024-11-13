namespace Application.TenantConfigs.Queries.GeTenantConfigs
{
    public record GetTenantConfigsResponse(
        Guid TenantId,
        Guid TenantConfigId,
        string ConfigName,
        string ConfigValue,
        string ConfigType,
        bool Enabled
    );
}
