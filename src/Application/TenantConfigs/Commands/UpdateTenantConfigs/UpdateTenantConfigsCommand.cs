using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Commands.UpdateTenantConfigs
{
    public record UpdateTenantConfigsCommand(
        IEnumerable<ConfigsDto> Configs
    ) : IRequest<Result<UpdateTenantConfigsResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class ConfigsDto
    {
        public Guid Configid { get; set; }
        public string ConfigValue { get; set; } = default!;
        public bool Enabled { get; set; }
    }

    public class UpdateTenantConfigsValidator : AbstractValidator<UpdateTenantConfigsCommand>
    {
        public UpdateTenantConfigsValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleForEach(x => x.Configs).SetValidator(new ConfigsDtoValidator());
        }
    }

    public class ConfigsDtoValidator : AbstractValidator<ConfigsDto>
    {
        public ConfigsDtoValidator()
        {
            RuleFor(x => x.Configid).RequireGuid();
            RuleFor(x => x.ConfigValue).ValidateRequiredProperty("el valor de la configuración");
        }
    }
}
