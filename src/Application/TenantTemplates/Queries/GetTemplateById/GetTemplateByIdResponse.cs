namespace Application.TenantTemplates.Queries.GetTemplateById
{
    public record GetTemplateByIdResponse(
        Guid TenantId,
        Guid TenantTemplateId,
        string Name,
        string Header,
        string Description,
        string Logo
    );
}
