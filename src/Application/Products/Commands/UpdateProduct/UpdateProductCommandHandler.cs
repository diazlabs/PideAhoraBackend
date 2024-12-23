﻿using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _productService;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IImageService _imageService;
        public UpdateProductCommandHandler(
            ApplicationContext context, IProductService productService, ILogger<UpdateProductCommandHandler> logger, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _context = context;
            _productService = productService;
            _logger = logger;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }
        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            bool includeChoices = true;
            Product? product = await _productService
                .FindProductById(request.ProductId, request.TenantId, cancellationToken, includeChoices);

            if (product == null)
            {
                return Result.NotFound();
            }

            if (request.Image != null)
            {
                string newImageId = Guid.NewGuid().ToString();
                product.Image = newImageId;
                await _imageService.UploadImageAsync(request.Image, product.Image);
            }

            string productString = JsonConvert.SerializeObject(product, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            });

            _logger.LogInformation("Updating product {@product} with request {@request}", productString, request);

            product.UpdatedAt = DateTime.UtcNow;
            product.Modifier = _currentUserProvider.GetUserId();
            product.ProductPrice = request.ProductPrice;
            product.ProductDescription = request.ProductDescription;
            product.ProductName = request.ProductName;
            product.Visible = request.Visible;

            product.ProductChoices = [];

            if (request.Choices.Count > 0)
            {
                product.ProductChoices = request.Choices.Select(choice => new ProductChoice
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
                        ProductChoiceId = choice.ProductChoiceId,
                        ProductId = o.ProductId,
                        ChoiceOptionId = o.ChoiceOptionId,
                        OptionPrice = o.OptionPrice,
                    }).ToList()
                }).ToList();
            }

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                _logger.LogInformation("Update product succeed {productId}", product.ProductId);

                return new UpdateProductResponse();
            }

            return Result.Error("Error al actualizar el producto.");
        }
    }
}
