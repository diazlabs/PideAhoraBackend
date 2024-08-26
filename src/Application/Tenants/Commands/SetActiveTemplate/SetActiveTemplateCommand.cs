using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTemplateCommand : IRequest<Result<SetActiveTemplateResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TemplateId { get; set; }
    }

    public class SetActiveTenantValidator : AbstractValidator<SetActiveTemplateCommand> 
    {
        public SetActiveTenantValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
        }
    }
}
