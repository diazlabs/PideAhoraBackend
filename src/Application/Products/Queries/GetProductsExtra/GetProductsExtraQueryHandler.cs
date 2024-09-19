using Application.Common.Persistence;
using Domain.Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductsExtra
{
    public class GetProductsExtraQueryHandler : IRequestHandler<GetProductsExtraQuery, IEnumerable<GetProductsExtraResponse>>
    {
        private readonly ApplicationContext _context;

        public GetProductsExtraQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetProductsExtraResponse>> Handle(GetProductsExtraQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(x => x.TenantId == request.TenantId && x.ProductType == ProductType.Extra.Type)
                .Select(x => new GetProductsExtraResponse(
                    x.ProductId,
                    x.TenantId,
                    x.ProductName,
                    x.ProductDescription,
                    x.Image,
                    x.ProductPrice,
                    x.Visible,
                    x.CreatedAt,
                    x.UpdatedAt
                ))
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
