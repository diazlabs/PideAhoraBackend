using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public record SetActiveTemplateCommand(Guid TenantId, Guid TenantTemplateId) : IRequest<Result<SetActiveTemplateResponse>>;

    public class SetActiveTenantValidator : AbstractValidator<SetActiveTemplateCommand> 
    {
        public SetActiveTenantValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
        }
    }
}
