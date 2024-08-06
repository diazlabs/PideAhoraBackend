using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Commands.DeleteTenant
{
    public class DeleteTenantCommand : IRequest<Result<DeleteTenantResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantValidator : AbstractValidator<DeleteTenantCommand>
    {
        public DeleteTenantValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
