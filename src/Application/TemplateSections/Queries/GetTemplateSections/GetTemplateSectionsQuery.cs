using Application.Common.Extensions;
using FluentValidation;
using MediatR;

namespace Application.TemplateSections.Queries.GetTemplateSections
{
    public class GetTemplateSectionsQuery : IRequest<IEnumerable<GetTemplateSectionsResponse>>
    {
        public Guid TenantId { get; set; }
        public Guid TenantTemplateId { get; set; }
    }

    public class GetTemplateSectionsValidator : AbstractValidator<GetTemplateSectionsQuery>
    {
        public GetTemplateSectionsValidator()
        {
            RuleFor(x => x.TenantId).RequireGuid();
            RuleFor(x => x.TenantTemplateId).RequireGuid();
        }
    }
}
