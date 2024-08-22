using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.ProductChoices.Commands.DeleteChoice
{
    public class DeleteChoiceCommand : IRequest<Result<DeleteChoiceResponse>>
    {
        public Guid TenantId { get; set; }
        public int ProductChoiceId { get; set; }
        public Guid DeletedBy { get; set; }
    }

    public class DeleteChoiceValidato : AbstractValidator<DeleteChoiceCommand>
    {
        public DeleteChoiceValidato()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.DeletedBy).RequireGuid();
            RuleFor(x => x.ProductChoiceId).GreaterThan(0).WithMessage("No es una elección válida");
        }
    }
}
