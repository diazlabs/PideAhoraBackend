using Application.Common.Interfaces;
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
        private readonly IImageService _imageService;
        public CreateProductCommandHandler(ApplicationContext context, ILogger<CreateProductCommandHandler> logger, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _context = context;
            _logger = logger;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }
        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            string? imageId = null;

            if(request.Image != null)
            {
                imageId = Guid.NewGuid().ToString();
                await _imageService.UploadImageAsync(request.Image, imageId);
            }
            
            Product product = new()
            {
                Creator = _currentUserProvider.GetUserId(),
                Deleted = false,
                Image = imageId,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                Visible = request.Visible,
                TenantId = request.TenantId,
                ProductPrice = request.ProductPrice,
                CreatedAt = DateTime.UtcNow,
                ProductType = request.ProductType,
            };

            _context.Products.Add(product);

            _logger.LogInformation("Creatring product {@product}", product);

            using var transaaction = _context.Database.BeginTransaction();
            try
            {
                int rows = await _context.SaveChangesAsync();
                if (rows <= 0)
                {
                    return Result.Error("Error al guardar el producto, intenta de nuevo.");
                }

                if (request.Choices != null && request.Choices.Count > 0)
                {
                    var result =  await CreateChoices(request.Choices, product.ProductId, cancellationToken);
                    if (!result.IsSuccess)
                    {
                        await transaaction.RollbackAsync();
                        return Result.Error("Error al guardar el producto, intenta de nuevo.");
                    }
                }

                await transaaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error when creating product {@request}", request);

                await transaaction.RollbackAsync();
                return Result.Error("Error al guardar el producto, intenta de nuevo.");
            }


            return new CreateProductResponse();
        }

        public async Task<Result> CreateChoices(List<ChoicesDto> choices, int productId, CancellationToken canellationToken)
        {
            _logger.LogInformation("Creatring choices {@choices} for productId {productId}", choices, productId);
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
                    ProductId = o.ProductId,
                    Visible = o.Visible,
                }).ToList(),
            }).ToList();


            _context.ProductChoices.AddRange(productChoices);
            int rows = await _context.SaveChangesAsync(canellationToken);
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error();
        }
    }
}
