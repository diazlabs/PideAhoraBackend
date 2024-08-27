using Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Email;
using Infrastructure.Common.Services;
using Infrastructure.Security.TokenGenerator;
using Infrastructure.Security.TokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using Application.Common.Security;
using Infrastructure.Security;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Logger.CreateLogger(configuration);

            services
                .AddHttpContextAccessor()
                .AddAuthentication(configuration)
                .AddEmailService(configuration);

            services.AddHealthChecks();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services
                .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            EmailSettings emailSettings = new();
            configuration.Bind(EmailSettings.Section, emailSettings);

            services
                .AddFluentEmail(emailSettings.DefaultFromEmail)
                .AddSmtpSender(new SmtpClient(emailSettings.SmtpSettings.Server)
                {
                    Port = emailSettings.SmtpSettings.Port,
                    Credentials = new NetworkCredential(
                        emailSettings.SmtpSettings.Username,
                        emailSettings.SmtpSettings.Password),
                });

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
