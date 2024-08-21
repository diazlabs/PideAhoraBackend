using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Result<CreateProductResponse>>
    {
        public Guid Creator {  get; set; }
        public Guid TenandId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? ProdcutDescription { get; set; }
        public string? Image { get; set; }
        public double ProductPrice { get; set; }
        public bool Visible { get; set; }
    }

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Creator).RequireGuid();
            RuleFor(x => x.TenandId).RequireGuid();
            RuleFor(x => x.ProductPrice).PriceGuard();
            RuleFor(x => x.ProdcutDescription).MinimumLength(0).MaximumLength(500);
            RuleFor(x => x.ProductName).ValidateRequiredProperty("el nombre del producto");
            RuleFor(x => x.Visible).NotNull();
        }
    }
}
