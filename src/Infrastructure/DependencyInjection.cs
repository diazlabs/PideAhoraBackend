using Application.Common.Interfaces;
using Domain.Entities;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using Infrastructure.Common.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Elastic.Channels;
using Serilog.Exceptions;
using Serilog.Filters;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithProperty("Application", "pidelo-api")
                .Filter.ByExcluding(
                Matching.WithProperty<string>("RequestPath", path =>
                {
                    string[] excludedPaths = ["/swagger", "/healthz"];

                    return excludedPaths.Any(x => path.StartsWith(x));
                }))
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch([new Uri(configuration["ElasticConfiguration:Uri"]!)],
                    opts =>
                    {
                        opts.DataStream = new DataStreamName($"pidelo-logs-{DateTime.UtcNow:yyyy-MM}");
                        opts.BootstrapMethod = BootstrapMethod.Failure;
                        opts.ConfigureChannel = channelOpts =>
                        {
                            channelOpts.BufferOptions = new BufferOptions
                            {
                                ExportMaxConcurrency = 10,
                            };
                        };
                })
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddHealthChecks();

            services.AddDbContext<ApplicationContext>(options =>
                options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention());

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddSignInManager<SignInManager<User>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITenantConfigRepository, TenantConfigRepository>();
            services.AddScoped<ITenantTemplateRepository, TenantTemplateRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
