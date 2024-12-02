using Application.Common.Interfaces;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplates
{
    public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, IEnumerable<GetTemplatesResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        public GetTemplatesQueryHandler(ITenantTemplateService tenantTemplateService)
        {
            _tenantTemplateService = tenantTemplateService;    
        }
        public async Task<IEnumerable<GetTemplatesResponse>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
            var templates = await _tenantTemplateService.GetTenantTemplatesByTenantId(request.TenantId);

            return templates.Select(x => new GetTemplatesResponse(
                x.TenantId,
                x.TenantTemplateId,
                x.Name,
                x.Header,
                x.Description,
                x.Logo
            )).ToList();
        }
    }
}
