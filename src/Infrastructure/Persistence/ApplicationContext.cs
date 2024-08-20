using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConfig> TenantConfigs { get; set; }
        public DbSet<TenantTemplate> TenantTemplates { get; set; }
        public DbSet<TemplateSection> TemplateSections { get; set; }
        public DbSet<SectionConfig> SectionConfigs { get; set; }
        public DbSet<SectionProduct> SectionProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductChoice> ProductChoices { get; set; }
        public DbSet<ChoiceOption> ChoiceOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailOption> OrderDetailOptions { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
