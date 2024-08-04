using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ValidationExceptionMiddleware>();

            return app;
        }
    }
}
