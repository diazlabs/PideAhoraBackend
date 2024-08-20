using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
    {
        public Guid TenantId { get; set; }
        public string? Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
        public string? OrderNotes { get; set; }
        public string DeliveryType { get; set; } = default!;
        public string? DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryNotes { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; } = default!;
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int? ProductDiscountId { get; set; }
        public int Quantity { get; set; }
        public List<OrderDetailOptionsDto>? OrderDetailOptions { get; set; }
    }

    public class OrderDetailOptionsDto
    {
        public int OptionId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
        }
    }
}
