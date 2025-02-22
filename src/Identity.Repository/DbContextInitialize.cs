using Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Repository
{
    public static class DbContextInitialize
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options => options.UseSqlite(connectionString));
            return services;

        }

        public static async Task AddMigrate(this WebApplication app)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Context>();
            await context.Database.MigrateAsync();

        }
    }
}
