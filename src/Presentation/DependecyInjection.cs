using System.Text.Json;

namespace Presentation
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
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
