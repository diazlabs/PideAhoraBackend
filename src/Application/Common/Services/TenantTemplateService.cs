using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class TenantTemplateService : ITenantTemplateService
    {
        private readonly ApplicationContext _context;
        public TenantTemplateService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Result<TenantTemplate>> Create(TenantTemplate tenantTemplate)
        {
            _context.TenantTemplates.Add(tenantTemplate);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return tenantTemplate;
            }

            return Result.Error("No se pudo crear el template, intente de nuevo.");
        }

        public async Task<Result> Delete(Guid tenantTempalteId)
        {
            TenantTemplate? template = await FindTenantTemplateById(tenantTempalteId);
            if (template == null)
            {
                return Result.NotFound();
            }

            template.Deleted = true;

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("No se pudo eliminar o puede que ya se haya eliminado, intente de nuevo.");
        }

        public async Task<TenantTemplate?> FindTenantTemplateById(Guid tenantTempalteId)
        {
            TenantTemplate? template = await _context.TenantTemplates.FindAsync(tenantTempalteId);
            return template;
        }

        public async Task<List<TenantTemplate>> GetTenantTemplatesByTenantId(Guid tenantId)
        {
            var templates = await _context.TenantTemplates
                .Where(x => x.TenantId == tenantId)
                .AsNoTracking()
                .ToListAsync();

            return templates;
        }

        public async Task<Result> Update(TenantTemplate tenantTemplate)
        {
            TenantTemplate? entityToUpdate = await FindTenantTemplateById(tenantTemplate.TenantTemplateId);
            if (entityToUpdate == null)
            {
                return Result.NotFound();
            }

            entityToUpdate = tenantTemplate;
            _context.TenantTemplates.Update(entityToUpdate);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("Error al actualziar, intente de nuevo.");

        }
    }
}
