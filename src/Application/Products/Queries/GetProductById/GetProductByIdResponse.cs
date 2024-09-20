namespace Application.Products.Queries.GetProductById
{
    public record GetProductByIdResponse(
        int ProductId,
        Guid TenantId,
        string ProductName,
        string ProductType,
        string? ProductDescription,
        string? Image,
        double ProductPrice,
        bool Visible,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        IEnumerable<ProductChoiceDto> Choices
    );


    public record ProductChoiceDto(
        int ProductChoiceId,
        int ProductId,
        string Choice,
        int Quantity,
        bool Required,
        bool Visible,  
        IEnumerable<ProductChoiceOptionsDto> Options
    );

    public record ProductChoiceOptionsDto(
        int ChoiceOptionId,
        int ProductChoiceId,
        int ProductId,
        double OptionPrice,
        bool Visible
    );
}
