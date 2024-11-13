using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.TenantConfigs.Queries.GeTenantConfigs
{
    public class GetTenantConfigsQuery : IRequest<IEnumerable<GetTenantConfigsResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class GeTenantConfigsQueryValidator : AbstractValidator<GetTenantConfigsQuery>
    {
        public GeTenantConfigsQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
