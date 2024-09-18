using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Commands.CreateTenantConfig
{
    public class CreateTenantConfigCommand : IRequest<Result<CreateTenantConfigResponse>>
    {
        public Guid TenantId { get; set; }
        public string ConfigName { get; set; } = default!;
        public string ConfigValue { get; set; } = default!;
        public string ConfigType { get; set; } = default!;
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public Guid Creator { get; set; }
    }

    public class CreateTenantConfigValidator : AbstractValidator<CreateTenantConfigCommand>
    {
        public CreateTenantConfigValidator()
        {
            RuleFor(x => x.ConfigValue).ValidateRequiredProperty("el valor de la configuración");
            RuleFor(x => x.ConfigName).ValidateRequiredProperty("el nombre de la configuración");
            RuleFor(x => x.Creator).RequireGuid();
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ConfigType).ValidateTenantConfigType();
        }
    }
}
