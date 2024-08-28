using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Queries.GetTenantById
{
    public record GetTenantByIdQuery(Guid TenantId) : IRequest<Result<GetTenantByIdResponse>>;

    public class GetTenantByIdValidator : AbstractValidator<GetTenantByIdQuery>
    {
        public GetTenantByIdValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
