﻿using Application.Common.Interfaces;
using Application.Common.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Product?> FindProductById(int productId, Guid tenantId, CancellationToken cancellationToken = default)
        {
            Product? product = await _context.Products
                .Where(x => x.ProductId == productId && x.TenantId == tenantId && !x.Deleted)
                .FirstOrDefaultAsync(cancellationToken);

            return product;
        }

        public async Task<bool> ProductExist(int productId, Guid tenantId, CancellationToken cancellationToken = default)
        {
            bool productExist = await _context.Products
               .AnyAsync(x => x.ProductId == productId && x.TenantId == tenantId && !x.Deleted, cancellationToken);

            return productExist;
        }
    }
}