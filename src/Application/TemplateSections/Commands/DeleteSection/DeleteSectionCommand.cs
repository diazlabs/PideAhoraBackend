using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Commands.DeleteSection
{
    public class DeleteSectionCommand : IRequest<Result>
    {
        public Guid DeletedBy { get; set; }
        public Guid TenantId { get; set; }
        public int SectionId { get; set; }
        public Guid TemplateId { get; set; }
    }

    public class DeleteSectionValidator : AbstractValidator<DeleteSectionCommand>
    {
        public DeleteSectionValidator()
        {
            RuleFor(x => x.DeletedBy).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
            RuleFor(x => x.SectionId).GreaterThan(0).WithMessage("No es un Id válido");
        }
    }
}
