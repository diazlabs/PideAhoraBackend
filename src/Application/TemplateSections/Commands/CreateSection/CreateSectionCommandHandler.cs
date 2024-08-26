using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Result<CreateSectionResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ITenantTemplateService _tenantTemplateService;
        public CreateSectionCommandHandler(ApplicationContext context, ITenantTemplateService tenantTemplateService)
        {
            _context = context;
            _tenantTemplateService = tenantTemplateService;
        }
        public async Task<Result<CreateSectionResponse>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate? template = await _tenantTemplateService.FindTenantTemplateById(request.TemplateId, request.TenantId, cancellationToken);

            if (template == null)
            {
                return Result.NotFound();
            }

            var sections = await _context.TemplateSections.ToListAsync();

            TemplateSection section = new()
            {
                CreatedAt = DateTime.UtcNow,
                Creator = request.Creator,
                Order = sections.Any() ? sections.Max(x => x.Order) + 1 : 0,
                SectionVariantId = request.SectionVariantId,
                TemplateId = request.TemplateId,
                Visible = request.Visible,
                SectionConfigs = [],
                SectionProducts = request.Products.Select(x => new SectionProduct
                {
                    Order = x.Order,
                    ProductId = x.ProductId,
                }).ToList()
            };

            _context.TemplateSections.Add(section);
            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new CreateSectionResponse();
            }

            return Result.Error("Error al guardar la sección");
        }
    }
}
