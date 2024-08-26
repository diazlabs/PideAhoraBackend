using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Persistence
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .ToTable("users")
                .Property(x => x.Id).HasColumnName("user_id");

            builder.Entity<Role>()
                .ToTable("roles")
                .Property(x => x.Id).HasColumnName("role_id");

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("role_claims")
                .Property(x => x.Id).HasColumnName("role_claim_id");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("user_roles");

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("user_claims")
                .Property(x => x.Id).HasColumnName("user_claims_id");

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("user_logins");

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("user_tokens");

            builder.Entity<SectionProduct>()
                .HasAlternateKey(x => new { x.Order, x.ProductId });
        }
    }
}
