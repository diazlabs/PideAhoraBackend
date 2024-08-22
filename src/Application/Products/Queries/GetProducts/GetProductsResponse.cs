namespace Application.Products.Queries.GetProducts
{
    public record GetProductsResponse(
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
