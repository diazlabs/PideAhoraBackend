using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _productService;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;
        public UpdateProductCommandHandler(
            ApplicationContext context, IProductService productService, ILogger<UpdateProductCommandHandler> logger, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _productService = productService;
            _logger = logger;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productService.FindProductById(request.ProductId, request.TenantId, cancellationToken, true);

            if (product == null)
            {
                return Result.NotFound();
            }

            _logger.LogInformation("Updating product {@product} with request {@request}", product, request);

            product.UpdatedAt = DateTime.UtcNow;
            product.Modifier = _currentUserProvider.GetUserId();
            product.ProductPrice = request.ProductPrice;
            product.ProductDescription = request.ProductDescription;
            product.ProductName = request.ProductName;
            product.Visible = request.Visible;
            product.Image = request.Image;

            product.ProductChoices = [];

            if (request.Choices?.Count > 0)
            {
                foreach (var choice in request.Choices)
                {
                    ProductChoice productChoice = new()
                    {
                        ProductChoiceId = choice.ProductChoiceId,
                        Choice = choice.Choice,
                        ProductId = product.ProductId,
                        Quantity = choice.Quantity,
                        Required = choice.Required,
                        Visible = choice.Visible,
                        ChoiceOptions = choice.Options.Select(o => new ChoiceOption
                        {
                            Visible = o.Visible,
                            ChoiceId = choice.ProductChoiceId,
                            ProductId = product.ProductId,
                            ChoiceOptionId = o.ChoiceOptionId,
                            OptionPrice = o.OptionPrice,
                        }).ToList()
                    };

                    product.ProductChoices.Add(productChoice);
                }
            }

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                _logger.LogInformation("Update product succeed {@product}", product);

                return new UpdateProductResponse();
            }

            return Result.Error("Error al actualizar el producto.");
        }
    }
}
