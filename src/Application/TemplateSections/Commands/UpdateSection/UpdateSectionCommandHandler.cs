using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Commands.UpdateSection
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Result<UpdateSectionResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ITenantTemplateService _tenantTemplateService;
        public UpdateSectionCommandHandler(ApplicationContext context, ITenantTemplateService tenantTemplateService)
        {
            _context = context;
            _tenantTemplateService = tenantTemplateService;
        }
        public async Task<Result<UpdateSectionResponse>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate? template = await _tenantTemplateService.FindTenantTemplateById(request.TemplateId, request.TenantId, cancellationToken);
            if (template == null)
            {
                return Result.NotFound();
            }

            var section = await _context.TemplateSections
                .Where(x => x.TemplateSectionId == request.TemplateSectionId && x.TemplateId == request.TemplateId)
                .FirstOrDefaultAsync(cancellationToken);

            if (section == null)
            {
                return Result.NotFound();
            }

            section.Visible = request.Visible;
            section.Modifier = request.Modifier;
            section.UpdatedAt = DateTime.UtcNow;
            section.SectionProducts = request.Products.Select(x => new SectionProduct
            {
                ProductId = x.ProductId,
                SectionProductId = x.SectionProductId,
                Order = x.Order,
                TemplateSectionId = section.TemplateSectionId,
            }).ToList();

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new UpdateSectionResponse();
            }

            return Result.Error("Error al actulizar la sección");
        }
    }
}
