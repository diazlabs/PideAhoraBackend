namespace Application.Products.Queries.GetProductById
{
    public record GetProductByIdResponse(
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
