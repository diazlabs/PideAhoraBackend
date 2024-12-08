
namespace Application.TemplateSections.Queries.GetTemplateSectionById
{
    public record GetTemplateSectionByIdResponse(
        Guid TenantId,
        Guid TenantTemplateId,
        int TemplateSectionId, 
        string SectionName, 
        string SectionDescription,
        IEnumerable<SectionProductDto> Products,
        IEnumerable<SectionConfigurationDto> Configurations
    );

    public record SectionProductDto(
        int SectionProductId,
        int ProductId,
        string ProductName,
        string? Image,
        int Order
    );

    public record SectionConfigurationDto(
        int SectionConfigId,
        string SectionConfigName,
        string SectionConfigValue
    );
}
