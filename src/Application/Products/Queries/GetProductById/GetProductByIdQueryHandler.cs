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
            bool includeChoices = true;

            Product? product = await _productService
                .FindProductById(request.ProductId, request.TenantId, cancellationToken, includeChoices);

            if (product == null)
            {
                return Result.NotFound();
            }

            return new GetProductByIdResponse(
                product.ProductId,
                product.TenantId,
                product.ProductName,
                product.ProductType,
                product.ProductDescription,
                product.Image,
                product.ProductPrice,
                product.Visible,
                product.CreatedAt,
                product.UpdatedAt,
                product.ProductChoices.Select(x => new ProductChoiceDto(
                    x.ProductChoiceId,
                    x.ProductId,
                    x.Choice,
                    x.Quantity,
                    x.Required,
                    x.Visible,
                    x.ChoiceOptions.Select(y => new ProductChoiceOptionsDto(
                        y.ChoiceOptionId,
                        y.ProductChoiceId,
                        y.ProductId,
                        y.OptionPrice,
                        y.Visible
                    ))
                ))
            );
        }
    }
}
