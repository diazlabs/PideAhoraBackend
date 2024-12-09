using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Commands.ArrangeSectionsOrder
{
    public class ArrangeSectionsOrderCommand : IRequest<Result<ArrangeSectionsOrderResponse>>
    {
        public Guid TenantTemplateId { get; set; }
        public List<ArrangeSectionsOrderRequest> Sections { get; set; } = [];
    }

    public class ArrangeSectionsOrderRequest
    {
        public int TemplateSectionId { get; set; }
        public int Order { get; set; }
    }

    public class ArrangeSectionsOrderCommandValidator : AbstractValidator<ArrangeSectionsOrderCommand>
    {
        public ArrangeSectionsOrderCommandValidator()
        {
            RuleFor(x => x.TenantTemplateId).RequireGuid();
            RuleFor(x => x.Sections).NotEmpty().WithMessage("No hay ninguna seccion por ordernar.");
        }
    }
}
