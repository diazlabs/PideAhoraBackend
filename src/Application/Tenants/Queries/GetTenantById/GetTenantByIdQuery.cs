using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQuery : IRequest<Result<GetTenantByIdResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class GetTenantByIdValidator : AbstractValidator<GetTenantByIdQuery>
    {
        public GetTenantByIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
