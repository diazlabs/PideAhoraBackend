namespace Application.TenantTemplates.Queries.GetTemplates
{
    public record GetTemplatesResponse(
        Guid TenantId,
        Guid TenantTemplateId,
        string Name,
        string Header,
        string Description,
        string Logo,
        IEnumerable<TemplateSectionDto> Sections
    );

    public record TemplateSectionDto(
        Guid TenantTemplateId,
        int TemplateSectionId,
        string SectionName,
        string SectionDescription,
        int Order
    );
}
