using Application.Common.Extensions;
using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Common.Enums;
using FluentValidation;
using MediatR;
using System.Linq;

namespace Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
    {
        public Guid TenantId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
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
        public int Quantity { get; set; }
        public int? ProductDiscountId { get; set; }
        public List<OrderDetailOptionsDto>? OrderDetailOptions { get; set; }
    }

    public class OrderDetailOptionsDto
    {
        public int OptionId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IOrderService _orderService;
        public CreateOrderCommandValidator(IOrderService orderService)
        {
            _orderService = orderService;

            RuleFor(x => x.Name)
                .ValidateRequiredProperty("tu nombre")
                .When(x => !x.UserId.HasValue);

            RuleFor(x => x.PhoneNumber)
                .ValidatePhoneNumber()
                .When(x => !x.UserId.HasValue);

            RuleFor(x => x.Email)
                .ValidateRequiredProperty("tu nombre")
                .When(x => !x.UserId.HasValue);

            RuleFor(x => x.DeliveryAddress)
                .ValidateRequiredProperty("tu nombre")
                .When(x => !x.UserId.HasValue);

            RuleFor(x => x.DeliveryType)
                .Must(x => x == DeliveryTypes.AtHome || x == DeliveryTypes.PickUp)
                .WithMessage("No es un tipo entrega correcto");

            RuleFor(x => x.OrderNotes)
                .MaximumLength(300)
                .WithMessage("La nota es muy larga, lo maximo son 300 caracteres.");

            RuleFor(x => x.DeliveryNotes)
                .MaximumLength(300)
                .WithMessage("La nota es muy larga, lo maximo son 300 caracteres.");

            RuleFor(x => x.OrderDetails)
                .NotNull()
                .WithMessage("Debes ordernar al menos un producto")
                .NotEmpty()
                .WithMessage("Debes ordernar al menos un producto");

            RuleForEach(x => x.OrderDetails)
                .MustAsync(async (productDiscount, cancellation) =>
                {
                    if (productDiscount.ProductDiscountId == null) return true;

                    return await _orderService.ValidateDiscount(productId: productDiscount.ProductId, discountId: (int)productDiscount.ProductDiscountId!);
                }).SetValidator(new DetailsValidator(_orderService));

            RuleForEach(x => x.OrderDetails)
                .MustAsync(async (detail, cancellation) =>
                {
                    var options = detail.OrderDetailOptions?
                            .Select(x => (optionId: x.OptionId, quantity: x.Quantity)) 
                            ?? [];

                    return await _orderService.ValidateDetailOptions(
                        detail.ProductId,
                        options
                    );
                })
                .WithMessage("Los productos no hacen match con las opciones disponibles");
        }
    }

    public class DetailsValidator : AbstractValidator<OrderDetailDto>
    {
        private readonly IOrderService _orderService;
        public DetailsValidator(IOrderService orderService)
        {
            _orderService = orderService;

            RuleFor(x => x.Quantity).GreaterThan(x => 0).WithMessage("Debes de ingresar una cantidad mayor a cero para este producto");

            RuleFor(x => x.ProductDiscountId).GreaterThan(x => 0)
                .WithMessage("El descuento ingresado es inválido")
                .When(x => x.ProductDiscountId.HasValue);

            RuleFor(x => x.ProductId).GreaterThan(x => 0).WithMessage("El producto ingresado es inválido");

            RuleForEach(x => x.OrderDetailOptions)
                .SetValidator(new OptionsValidator())
                .When(x => x.OrderDetailOptions?.Count > 0);
        }
    }

    public class OptionsValidator : AbstractValidator<OrderDetailOptionsDto>
    {
        public OptionsValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(x => 0).WithMessage("Debes de ingresar una cantidad mayor a cero para este producto");
            RuleFor(x => x.OptionId).GreaterThan(x => 0).WithMessage("La opción seleccionada es inválida");
        }
    }
}
