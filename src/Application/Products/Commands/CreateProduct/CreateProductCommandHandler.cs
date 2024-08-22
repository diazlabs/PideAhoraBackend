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
            var currentDate = DateTime.UtcNow;
            var choices = request.Choices?.Select(c => new ProductChoice
            {
                Choice = c.Choice,
                CreatedAt = currentDate,
                Creator = request.Creator,
                Quantity = c.Quantity,
                Required = c.Required,
                Visible = c.Visible,
                ChoiceOptions = c.Options.Select(o => new ChoiceOption
                {
                    CreatedAt = currentDate,
                    Creator = request.Creator,
                    OptionPrice = o.OptionPrice,
                    ProductId = o.ProductId,
                    Visible = o.Visible,
                }).ToList(),
            }).ToList();

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
                ProductChoices = choices,
                CreatedAt = currentDate,
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
