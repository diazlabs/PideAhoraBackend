namespace Application.TemplateSections.Queries.GetTemplateSections
{
    public record GetTemplateSectionsResponse(
        int SectionVariantId,
        int Order,
        bool Visible
    );
}
