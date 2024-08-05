using Application.Common.Extensions;
using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public class GetTenantsByUserQuery : IRequest<Result<GetTenantsByUserResponse>>
    {
        public Guid UserId { get; set; }
    }

    public class GetTenantsByUserValidator : AbstractValidator<GetTenantsByUserQuery>
    {
        public GetTenantsByUserValidator()
        {
            RuleFor(x => x.UserId).RequireGuid();    
        }
    }
}
