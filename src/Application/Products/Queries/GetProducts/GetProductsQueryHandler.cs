using Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
    {
        private readonly ApplicationContext _context;
        public GetProductsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(x => x.TenantId == request.TenantId && !x.Deleted)
                .Select(x => new GetProductsResponse(
                    x.ProductId,
                    x.TenantId,
                    x.ProductName,
                    x.ProductType,
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
