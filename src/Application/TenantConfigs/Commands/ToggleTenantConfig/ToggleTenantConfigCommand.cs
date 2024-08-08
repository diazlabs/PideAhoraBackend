using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Commands.ToggleTenantConfig
{
    public class ToggleTenantConfigCommand : IRequest<Result<ToggleTenantConfigResponse>>
    {
        public Guid TenantConfigId { get; set; }
        public bool Enabled { get; set; }
    }

    public class ToggleTenantConfigValidator : AbstractValidator<ToggleTenantConfigCommand>
    {
        public ToggleTenantConfigValidator()
        {
            RuleFor(x => x.TenantConfigId).RequireGuid();
            RuleFor(x => x.Enabled).NotNull();
        }
    }
}
