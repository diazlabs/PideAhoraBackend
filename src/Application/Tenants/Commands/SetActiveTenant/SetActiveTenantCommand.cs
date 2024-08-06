using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTenantCommand : IRequest<Result<SetActiveTenantResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TemplateId { get; set; }
    }

    public class SetActiveTenantValidator : AbstractValidator<SetActiveTenantCommand> 
    {
        public SetActiveTenantValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TemplateId).RequireGuid();
        }
    }
}
