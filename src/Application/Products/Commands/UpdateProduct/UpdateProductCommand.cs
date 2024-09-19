using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<UpdateProductResponse>>
    {
        public int ProductId { get; set; }
        public Guid TenantId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? ProductDescription { get; set; }
        public IFormFile? Image { get; set; }
        public string ProductType { get; set; } = default!;
        public double ProductPrice { get; set; }
        public bool Visible { get; set; }
        public List<ChoicesDto>? Choices { get; set; }
    }

    public class ChoicesDto
    {
        public int ProductChoiceId { get; set; }
        public string Choice { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Required { get; set; }
        public bool Visible { get; set; }
        public List<ChoiceOptionDto> Options { get; set; } = default!;
    }

    public class ChoiceOptionDto
    {
        public int ChoiceOptionId { get; set; }
        public double OptionPrice { get; set; }
        public bool Visible { get; set; }
    }

    public class UpdateProdcutValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProdcutValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ProductPrice).PriceGuard();
            RuleFor(x => x.ProductDescription).MinimumLength(0).MaximumLength(500);
            RuleFor(x => x.ProductName).ValidateRequiredProperty("el nombre del producto");
            RuleFor(x => x.Visible).NotNull();
            RuleFor(x => x.Image).ValidateImage();
            RuleFor(x => x.ProductType).ValidateProductType();

            RuleForEach(x => x.Choices).SetValidator(new ChoiceValidator());
        }
    }

    public class ChoiceValidator : AbstractValidator<ChoicesDto>
    {
        public ChoiceValidator()
        {
            RuleFor(x => x.Choice).ValidateRequiredProperty("texto de la elección");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad minima es 1");
            RuleFor(x => x.Choice).ValidateRequiredProperty("texto de la elección");
            RuleFor(x => x.Options)
                .NotEmpty()
                .WithMessage("Debes de ingresar al menos una opción")
                .NotNull()
                .WithMessage("Debes de ingresar al menos una opción");

            RuleForEach(x => x.Options).SetValidator(new ChoiceOptionValidator());
        }
    }

    public class ChoiceOptionValidator : AbstractValidator<ChoiceOptionDto>
    {
        public ChoiceOptionValidator()
        {
            RuleFor(x => x.OptionPrice).GreaterThanOrEqualTo(0).WithMessage("El producto debe poseer un precio mayor o igual a 0");
        }
    }
}
