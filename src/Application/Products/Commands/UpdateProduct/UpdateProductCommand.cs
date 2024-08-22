using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<UpdateProductResponse>>
    {
        public int ProductId { get; set; }
        public Guid Modifier { get; set; }
        public Guid TenantId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? ProductDescription { get; set; }
        public string? Image { get; set; }
        public double ProductPrice { get; set; }
        public bool Visible { get; set; }
    }

    public class UpdateProdcutValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProdcutValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.Modifier).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ProductPrice).PriceGuard();
            RuleFor(x => x.ProductDescription).MinimumLength(0).MaximumLength(500);
            RuleFor(x => x.ProductName).ValidateRequiredProperty("el nombre del producto");
            RuleFor(x => x.Visible).NotNull();
        }
    }
}
