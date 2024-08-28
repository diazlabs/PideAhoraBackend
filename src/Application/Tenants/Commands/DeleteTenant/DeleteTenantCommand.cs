using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Commands.DeleteTenant
{
    public record DeleteTenantCommand(Guid TenantId) : IRequest<Result<DeleteTenantResponse>>;

    public class DeleteTenantValidator : AbstractValidator<DeleteTenantCommand>
    {
        public DeleteTenantValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
