using Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Infrastructure.Security.TokenGenerator;
using Infrastructure.Security.TokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Logger.CreateLogger(configuration);

            services.AddAuthentication(configuration);

            services.AddHealthChecks();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services
                .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            return services;
        }
    }
}
