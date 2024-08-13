using HealthChecks.UI.Client;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Serilog;

namespace Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            app.UseMiddleware<ValidationExceptionMiddleware>();

            return app;
        }

        public static void MapHealthCheckz(this IEndpointRouteBuilder builder)
        {
            builder.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });
        }
    }
}
