using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Commands.DeleteSection
{
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Result>
    {
        private readonly ApplicationContext _context;
        private readonly ITenantTemplateService _tenantTemplateService;
        public DeleteSectionCommandHandler(ApplicationContext context, ITenantTemplateService tenantTemplateService)
        {
            _context = context;
            _tenantTemplateService = tenantTemplateService;
        }
        public async Task<Result> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate? template = await _tenantTemplateService.FindTenantTemplateById(request.TemplateId, request.TenantId, cancellationToken);
            if (template == null)
            {
                return Result.NotFound();
            }

            var section = await _context.TemplateSections
                .Where(x => x.TemplateSectionId == request.SectionId && x.TemplateId == request.TemplateId)
                .FirstOrDefaultAsync(cancellationToken);

            if (section == null)
            {
                return Result.NotFound();
            }

            section.DeletedAt = DateTime.UtcNow;
            section.DeletedBy = request.DeletedBy;

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("Error al eliminar la sección");
        }
    }
}
