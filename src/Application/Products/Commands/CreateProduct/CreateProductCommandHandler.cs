using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
    {
        private readonly ApplicationContext _context;
        public CreateProductCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Creator = request.Creator,
                Deleted = false,
                Image = request.Image,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                Visible = request.Visible,
                TenantId = request.TenantId,
                ProductPrice = request.ProductPrice,
            };

            _context.Products.Add(product);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return new CreateProductResponse();
            }

            return Result.Error("Error al guardar el producto, intenta de nuevo.");
        }
    }
}
