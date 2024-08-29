using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace Presentation
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id= "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.ConfigureHttpJsonOptions(options => {
                options.SerializerOptions.WriteIndented = true;
                options.SerializerOptions.IncludeFields = true;
                options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            });

            services.AddRouting(x => x.LowercaseUrls = true);
            services.AddControllers();

            return services;
        }
    }
}