﻿using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<DeleteProductResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(ApplicationContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<Result<DeleteProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productService.FindProductById(request.ProductId, request.TenantId, cancellationToken);
            if (product == null)
            {
                return Result.NotFound();
            }

            product.Deleted = true;
            product.DeletedBy = request.UserId;
            product.DeletedAt = DateTime.UtcNow;

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return new DeleteProductResponse();
            }

            return Result.Error("No se pudo eliminar, intenta de nuevo.");
        }
    }
}