using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Commands.DeleteTenantConfig
{
    public class DeleteTenantConfigCommand : IRequest<Result<DeleteTenantConfigResponse>>
    {
        public Guid TenantConfigId { get; set; }
    }

    public class DeleteTenantConfigValidator : AbstractValidator<DeleteTenantConfigCommand>
    {
        public DeleteTenantConfigValidator()
        {
            RuleFor(x => x.TenantConfigId).RequireGuid();
        }
    }
}
