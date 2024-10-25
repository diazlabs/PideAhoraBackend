namespace Application.Orders.Queries.GetOrdersByTenantId
{
     public record GetOrdersByTenantIdResponse(
         Guid OrderId,
         Guid TenantId,
         string? Name,
         string? PhoneNumber,
         string? Email,
         DateTime CreatedAt,
         Guid? UserId,
         string? OrderNotes,
         string DeliveryType,
         string? DeliveryAddress,
         DateTime? DeliveryDate,
         string? DeliveryNotes,
         string Status,
         double Total,
         IEnumerable<OrderDetailDto>? Details
    );

    public record OrderDetailDto(
        int OrderDetailId,
        Guid OrderId,
        int ProductId,
        string ProductName,
        string? Image,
        string? DiscountCode,
        double? DiscountAmount,
        int Quantity,
        double ProductPrice,
        IEnumerable<DetailOptionDto>? Options
    );

    public record DetailOptionDto(
        int OrderDetailOptionId,
        int OrderDetailId,
        int OptionId,
        string ProductName,
        int Quantity,
        double ProductPrice
    );
}
