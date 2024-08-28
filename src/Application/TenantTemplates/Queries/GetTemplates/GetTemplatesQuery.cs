using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplates
{
    public record GetTemplatesQuery(Guid TenantId) : IRequest<IEnumerable<GetTemplatesResponse>>;

    public class GetTemplatesQueryValidator : AbstractValidator<GetTemplatesQuery>
    {
        public GetTemplatesQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
