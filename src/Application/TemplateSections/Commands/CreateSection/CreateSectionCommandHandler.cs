﻿using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateSections.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Result<CreateSectionResponse>>
    {
        private readonly ApplicationContext _context;
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CreateSectionCommandHandler(ApplicationContext context, ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider)
        {
            _context = context;
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<CreateSectionResponse>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate? template = await _tenantTemplateService.FindTenantTemplateById(request.TenantTemplateId, request.TenantId, cancellationToken);

            if (template == null)
            {
                return Result.NotFound();
            }

            var sections = await _context.TemplateSections.Where(x => x.TenantTemplateId == request.TenantTemplateId).ToListAsync();

            TemplateSection section = new()
            {
                CreatedAt = DateTime.UtcNow,
                Creator = _currentUserProvider.GetUserId(),
                Order = sections.Any() ? sections.Max(x => x.Order) + 1 : 1,
                SectionVariantId = 0, //created by admin
                TenantTemplateId = request.TenantTemplateId,
                SectionDescription = request.SectionDescription,
                SectionName = request.SectionName,
                Visible = request.Visible,
                SectionProducts = request.Products.Select(x => new SectionProduct
                {
                    Order = x.Order,
                    ProductId = x.ProductId,
                }).ToList(),
                SectionConfigs = request.Configs.Select(x => new SectionConfig
                {
                    CreatedAt = DateTime.UtcNow,
                    Creator = _currentUserProvider.GetUserId(),
                    SectionConfigName = x.ConfigName,
                    SectionConfigValue = x.ConfigValue
                }).ToList(),
            };

            _context.TemplateSections.Add(section);
            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new CreateSectionResponse();
            }

            return Result.Error("Error al guardar la sección");
        }
    }
}
