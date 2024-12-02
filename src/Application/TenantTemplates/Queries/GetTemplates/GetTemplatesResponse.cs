namespace Application.TenantTemplates.Queries.GetTemplates
{
    public record GetTemplatesResponse(
        Guid TenantId,
        Guid TenantTemplateId,
        string Name,
        string Header,
        string Description,
        string Logo
    );
}
