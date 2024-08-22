using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _productService;
        public UpdateProductCommandHandler(ApplicationContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productService.FindProductById(request.ProductId, request.TenantId, cancellationToken);

            if (product == null)
            {
                return Result.NotFound();
            }

            product.UpdatedAt = DateTime.UtcNow;
            product.Modifier = request.Modifier;
            product.ProductPrice = request.ProductPrice;
            product.ProductDescription = request.ProductDescription;
            product.ProductName = request.ProductName;
            product.Visible = request.Visible;
            product.Image = request.Image;

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new UpdateProductResponse();
            }

            return Result.Error("Error al actualizar el producto.");
        }
    }
}
