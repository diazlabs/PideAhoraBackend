using Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            Logger.CreateLogger(configuration);
            services.AddHealthChecks();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
