namespace Application.Products.Queries.GetProductsExtra
{
    public record GetProductsExtraResponse(
        int ProductId,
        Guid TenantId,
        string ProductName,
        string? ProductDescription,
        string? Image,
        double ProductPrice,
        bool Visible,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
