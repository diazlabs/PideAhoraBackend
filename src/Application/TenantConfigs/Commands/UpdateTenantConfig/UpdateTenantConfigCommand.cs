using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Commands.UpdateTenantConfig
{
    public record UpdateTenantConfigCommand(
        string ConfigName,
        string ConfigValue,
        bool Enabled
    ) : IRequest<Result<UpdateTenantConfigResponse>>
    {
        public Guid TenantId { get; set; }
    } 

    public class UpdateTenantConfigValidator : AbstractValidator<UpdateTenantConfigCommand>
    {
        public UpdateTenantConfigValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.ConfigValue).ValidateRequiredProperty("el valor de la configuración");
            RuleFor(x => x.ConfigName).ValidateRequiredProperty("el nombre de la configuración");
        }
    }
}
