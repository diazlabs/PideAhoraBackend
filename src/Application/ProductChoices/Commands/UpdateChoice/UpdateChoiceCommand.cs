using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.ProductChoices.Commands.UpdateChoice
{
    public class UpdateChoiceCommand : IRequest<Result<UpdateChoiceResponse>>
    {
        public int ProductId { get; set; }
        public Guid TenantId { get; set; }
        public int ProductChoiceId { get; set; }
        public string Choice { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Required { get; set; }
        public Guid Modifier { get; set; }
    }

    public class UpdateChoiceValidator : AbstractValidator<UpdateChoiceCommand>
    {
        public UpdateChoiceValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("No es un producto válido");
            RuleFor(x => x.ProductChoiceId).GreaterThan(0).WithMessage("No es una elección válida");
            RuleFor(x => x.Choice).ValidateRequiredProperty("el texto de la elección");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("la cantidad minima es 1");
            RuleFor(x => x.Modifier).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
