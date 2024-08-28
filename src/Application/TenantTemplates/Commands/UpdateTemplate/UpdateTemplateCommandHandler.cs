using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantTemplates.Commands.UpdateTemplate
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Result<UpdateTemplateResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public UpdateTemplateCommandHandler(ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider)
        {
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Result<UpdateTemplateResponse>> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate tenantTemplate = new()
            {
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                Logo = request.Logo,
                Header = request.Header,
                TenantId = request.TenantId,
                Modifier = _currentUserProvider.GetUserId(),
                TenantTemplateId = request.TenantTemplateId,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _tenantTemplateService.Update(tenantTemplate);
            if (result.IsSuccess)
            {
                return new UpdateTemplateResponse();
            }

            return (dynamic)result;
        }
    }
}
