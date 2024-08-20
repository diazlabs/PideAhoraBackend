using Application.Common.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions
{
    public static class DatabaseExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationContext context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            context.Database.Migrate();
        }
    }
}
