using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplates
{
    public class GetTemplatesQuery : IRequest<IEnumerable<GetTemplatesResponse>>
    {
        public Guid TenantId { get; set; }
    }

    public class GetTemplatesQueryValidator : AbstractValidator<GetTemplatesQuery>
    {
        public GetTemplatesQueryValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
        }
    }
}
