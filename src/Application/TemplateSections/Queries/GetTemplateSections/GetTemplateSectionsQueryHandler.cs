using Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Queries.GetTemplateSections
{
    public class GetTemplateSectionsQueryHandler : IRequestHandler<GetTemplateSectionsQuery, IEnumerable<GetTemplateSectionsResponse>>
    {
        private readonly ApplicationContext _context;
        public GetTemplateSectionsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetTemplateSectionsResponse>> Handle(GetTemplateSectionsQuery request, CancellationToken cancellationToken)
        {
            var templateSections = await _context.TemplateSections
                .Where(x => x.TenantTemplateId == request.TenantTemplateId && x.TenantTemplate.TenantId == request.TenantId && !x.Deleted)
                .Select(x => new GetTemplateSectionsResponse(
                    x.SectionVariantId,
                    x.Order,
                    x.Visible,
                    x.SectionName,
                    x.SectionDescription
                )).ToListAsync();

            return templateSections;
        }
    }
}
