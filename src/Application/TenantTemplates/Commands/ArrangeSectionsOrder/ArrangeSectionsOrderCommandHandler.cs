using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.TenantTemplates.Commands.ArrangeSectionsOrder
{
    public class ArrangeSectionsOrderCommandHandler : IRequestHandler<ArrangeSectionsOrderCommand, Result<ArrangeSectionsOrderResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ArrangeSectionsOrderCommandHandler> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;

        public ArrangeSectionsOrderCommandHandler(ICurrentUserProvider currentUserProvider, ApplicationContext context, ILogger<ArrangeSectionsOrderCommandHandler> logger)
        {
            _currentUserProvider = currentUserProvider;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<ArrangeSectionsOrderResponse>> Handle(ArrangeSectionsOrderCommand request, CancellationToken cancellationToken)
        {
            List<TemplateSection> sections = await _context.TemplateSections
                .Where(x => x.TenantTemplateId == request.TenantTemplateId && !x.Deleted)
                .ToListAsync();

            if (sections.Count < 1)
            {
                return Result.NotFound();
            }

            Guid userId = _currentUserProvider.GetUserId();

            _logger.LogInformation("Arranging Section Order for {teanntTemplateId} {@originalOrder} with {@request} ", request.TenantTemplateId, sections, request);

            foreach (var section in sections)
            {
                section.Order = request.Sections
                    .FirstOrDefault(x => x.TemplateSectionId == section.TemplateSectionId)
                    ?.Order ?? section.Order;

                section.Modifier = userId;
                section.UpdatedAt = DateTime.UtcNow;
            }

            bool isValid = sections.GroupBy(x => x.Order)
                .Select(x => new { Order = x.Key, Products = x })
                .All(x => x.Products.Count() == 1);

            if (!isValid)
            {
                return Result.Error("No se puede tener dos secciones con el mismo orden");
            }

            int rowsAffected = await _context.SaveChangesAsync(cancellationToken);
            if (rowsAffected > 0)
            {
                _logger.LogInformation("Sections order changed succesfully for {tenatTemplateId}", request.TenantTemplateId);
                return Result.Success();
            }

            return Result.Error("Error al guardar los datos");
        }
    }
}
