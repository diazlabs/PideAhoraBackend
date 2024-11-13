using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.ProductChoices.Commands.CreateChoice
{
    public class CreateChoiceCommand : IRequest<Result<CreateChoiceResponse>>
    {
        public int ProductId { get; set; }
        public Guid TenantId { get; set; }
        public string Choice { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Required { get; set; }
    }

    public class CreateChoiceValidator : AbstractValidator<CreateChoiceCommand>
    {
        public CreateChoiceValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.Choice).ValidateRequiredProperty("el texto de la elección");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("la cantidad minima es 1");
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
