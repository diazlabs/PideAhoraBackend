using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {
        private readonly IProductService _productService;
        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _productService.FindProductById(request.ProductId, request.TenantId, cancellationToken, true);

            if (product == null)
            {
                return Result.NotFound();
            }

            return new GetProductByIdResponse(
                product.ProductId,
                product.TenantId,
                product.ProductName,
                product.ProductDescription,
                product.Image,
                product.ProductPrice,
                product.Visible,
                product.CreatedAt,
                product.UpdatedAt
            );
        }
    }
}
