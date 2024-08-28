using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Commands.DeleteTemplate
{
    public record DeleteTemplateCommand(Guid TenantId, Guid TenantTemplateId) : IRequest<Result>;

    public class DeleteTemplateValidator : AbstractValidator<DeleteTemplateCommand>
    {
        public DeleteTemplateValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
        }
    }
}
