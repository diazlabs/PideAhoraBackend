using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Services;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddServices()
                .AddPersistence(configuration);

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention());

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddSignInManager<SignInManager<User>>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ITenantTemplateService, TenantTemplateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantConfigService, TenantConfigService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
