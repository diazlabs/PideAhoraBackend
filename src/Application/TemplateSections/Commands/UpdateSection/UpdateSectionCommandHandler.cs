using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.TemplateSections.Commands.UpdateSection
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Result<UpdateSectionResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger<UpdateSectionCommandHandler> _logger;
        public UpdateSectionCommandHandler(ApplicationContext context, ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider, ILogger<UpdateSectionCommandHandler> logger)
        {
            _context = context;
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
            _logger = logger;
        }
        public async Task<Result<UpdateSectionResponse>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate? template = await _tenantTemplateService.FindTenantTemplateById(request.TenantTemplateId, request.TenantId, cancellationToken);
            if (template == null)
            {
                return Result.NotFound();
            }

            var section = await _context.TemplateSections
                .Include(x => x.SectionProducts)
                .Include(x => x.SectionConfigs)
                .Where(x => x.TemplateSectionId == request.TemplateSectionId && x.TenantTemplateId == request.TenantTemplateId)
                .FirstOrDefaultAsync(cancellationToken);

            if (section == null)
            {
                return Result.NotFound();
            }

            string sectionString = JsonConvert.SerializeObject(section, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            });

            _logger.LogInformation(
                "Updating section {tenantId}, {tenantTemplateId}, {templateSectionId}, original {@originalConfigs} and new values {@request}",
                request.TenantId,
                request.TenantTemplateId,
                request.TemplateSectionId,
                sectionString,
                request
            );

            section.Visible = request.Visible;
            section.Modifier = _currentUserProvider.GetUserId();
            section.UpdatedAt = DateTime.UtcNow;
            section.SectionName = request.SectionName;
            section.SectionDescription = request.SectionDescription ?? section.SectionDescription;
            section.SectionProducts = request.Products.Select(x => new SectionProduct
            {
                ProductId = x.ProductId,
                SectionProductId = x.SectionProductId,
                Order = x.Order,
                TemplateSectionId = section.TemplateSectionId,
            }).ToList();

            foreach (var config in request.Configs)
            {
                var configToUpdate = section.SectionConfigs.FirstOrDefault(x => x.SectionConfigId == config.SectionConfigId);

                if (configToUpdate == null)
                {
                    continue;
                }

                configToUpdate.SectionConfigValue = config.ConfigValue;
                configToUpdate.Modifier = _currentUserProvider.GetUserId();
                configToUpdate.UpdatedAt = DateTime.UtcNow;
            }

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new UpdateSectionResponse();
            }

            return Result.Error("Error al actulizar la sección");
        }
    }
}
