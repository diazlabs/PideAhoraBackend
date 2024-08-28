using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CreateProductCommandHandler(ApplicationContext context, ILogger<CreateProductCommandHandler> logger, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _logger = logger;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var currentDate = DateTime.UtcNow;
            
            Product product = new()
            {
                Creator = _currentUserProvider.GetUserId(),
                Deleted = false,
                Image = request.Image,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                Visible = request.Visible,
                TenantId = request.TenantId,
                ProductPrice = request.ProductPrice,
                CreatedAt = currentDate,
            };

            _context.Products.Add(product);

            _logger.LogInformation("Creatring product {product}", product);

            using var transaaction = _context.Database.BeginTransaction();
            try
            {
                int rows = await _context.SaveChangesAsync();
                if (rows < 0)
                {
                    return Result.Error("Error al guardar el producto, intenta de nuevo.");
                }

                if (request.Choices != null)
                {
                    var result =  await CreateChoices(request.Choices, product.ProductId, cancellationToken);
                    if (!result.IsSuccess)
                    {
                        transaaction.Rollback();
                        return Result.Error("Error al guardar el producto, intenta de nuevo.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error when creating product {request}", request);

                transaaction.Rollback();
                return Result.Error("Error al guardar el producto, intenta de nuevo.");
            }


            return new CreateProductResponse();
        }

        public async Task<Result> CreateChoices(List<ChoicesDto> choices, int productId, CancellationToken canellationToken)
        {
            _logger.LogInformation("Creatring choices {choices} for productId {productId}", choices, productId);
            var productChoices = choices.Select(c => new ProductChoice
            {
                Choice = c.Choice,
                Quantity = c.Quantity,
                Required = c.Required,
                Visible = c.Visible,
                ProductId = productId,
                ChoiceOptions = c.Options.Select(o => new ChoiceOption
                {
                    OptionPrice = o.OptionPrice,
                    ProductId = productId,
                    Visible = o.Visible,
                }).ToList(),
            }).ToList();

            int rows = await _context.SaveChangesAsync(canellationToken);
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error();
        }
    }
}
