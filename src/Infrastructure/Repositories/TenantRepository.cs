﻿using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationContext _context;
        public TenantRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<Tenant>> Create(Tenant tenant)
        {
            _context.Tenants.Add(tenant);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return tenant;
            }

            return Result.Error("Error al crear la tienda, intenta de nuevo.");
        }

        public async Task<Result> Delete(Guid tenantId)
        {
           Tenant? tenant = await FindTenantById(tenantId);
            if (tenant == null)
            {
                return Result.NotFound();
            }

            tenant.Deleted = true;

            int rows = await _context.SaveChangesAsync();
            if ( rows > 0 )
            {
                return Result.Success();
            }

            return Result.Error("No se pudo eliminar, intenta de nuevo.");
        }

        public async Task<Tenant?> FindTenantById(Guid tenantId)
        {
            Tenant? tenant = await _context.Tenants.FindAsync(tenantId);
            return tenant;
        }

        public async Task<List<Tenant>> GetTenantsByUserId(Guid userId)
        {
            var tenants = await _context.Tenants
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return tenants;
        }

        public async Task<Result> SetActiveTemplateForTenantId(Guid tenantId, Guid tenantTemplateId)
        {
            Tenant? tenant = await FindTenantById(tenantId);
            if (tenant == null)
            {
                return Result.NotFound();
            }

            tenant.ActiveTemplateId = tenantTemplateId;
            int rows = await _context.SaveChangesAsync();
            if ( rows > 0 )
            {
                return Result.Success();
            }

            return Result.Error("Error al acitvar el template");
        }

        public async Task<Result> Update(Tenant tenant)
        {
            Tenant? tenantToUpdate = await FindTenantById(tenant.TenantId);
            if (tenantToUpdate == null) 
            {
                return Result.NotFound();
            }

            tenantToUpdate = tenant;
            _context.Tenants.Update(tenantToUpdate);

            int rows = await _context.SaveChangesAsync();
            if ( rows > 0 )
            {
                return Result.Success();
            }

            return Result.Error("Error al actualizar la tienda, intenta de nuevo.");
        }
    }
}