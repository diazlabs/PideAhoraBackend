using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Commands.DeleteTemplate
{
    public class DeleteTemplateCommand : IRequest<Result>
    {
        public Guid TenantId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid DeletedBy { get; set; }
    }

    public class DeleteTemplateValidator : AbstractValidator<DeleteTemplateCommand>
    {
        public DeleteTemplateValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
            RuleFor(x => x.DeletedBy).RequireGuid();
        }
    }
}
