using Application.Common.Persistence;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Queries.GetTemplateSectionById
{
    public class GetTemplateSectionByIdQueryHandler : IRequestHandler<GetTemplateSectionByIdQuery, Result<GetTemplateSectionByIdResponse>>
    {
        private readonly ApplicationContext _context;
        public GetTemplateSectionByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<GetTemplateSectionByIdResponse>> Handle(GetTemplateSectionByIdQuery request, CancellationToken cancellationToken)
        {
            GetTemplateSectionByIdResponse? tempalte = await _context.TemplateSections
                .Include(x => x.SectionProducts)
                .Include(x => x.SectionConfigs)
                .Where(x => !x.Deleted && x.TenantTemplateId == request.TenantTemplateId)
                .Select(x => new GetTemplateSectionByIdResponse(
                    request.TenantId,
                    x.TenantTemplateId,
                    x.TemplateSectionId,
                    x.SectionName,
                    x.SectionDescription,
                    x.Visible,
                    x.SectionProducts.Select(p => new SectionProductDto(
                        p.SectionProductId,
                        p.ProductId,
                        p.Product.ProductName,
                        p.Product.Image,
                        p.Order
                    )),
                    x.SectionConfigs.Select(c => new SectionConfigurationDto(
                        c.SectionConfigId,
                        c.SectionConfigName,
                        c.SectionConfigValue
                    ))
                ))
                .FirstOrDefaultAsync(cancellationToken);
            
            if (tempalte == null)
            {
                return Result.NotFound();
            }

            return tempalte;
        }
    }
}
