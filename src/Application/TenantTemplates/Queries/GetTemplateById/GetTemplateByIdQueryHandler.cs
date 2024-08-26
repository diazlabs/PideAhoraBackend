using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.TenantTemplates.Queries.GetTemplateById
{
    public class GetTemplateByIdQueryHandler : IRequestHandler<GetTemplateByIdQuery, Result<GetTemplateByIdResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        public GetTemplateByIdQueryHandler(ITenantTemplateService templateService)
        {
            _tenantTemplateService = templateService;
        }
        public async Task<Result<GetTemplateByIdResponse>> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var template = await _tenantTemplateService.FindTenantTemplateById(request.TenantTemplateId, request.TenantId);

            if (template == null)
            {
                return Result.NotFound();
            }

            return new GetTemplateByIdResponse();
        }
    }
}
