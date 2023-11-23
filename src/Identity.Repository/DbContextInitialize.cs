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


			//Configuração do identity server
			services.AddDefaultIdentity<User>()
						  .AddRoles<IdentityRole>()
						  .AddErrorDescriber<IdentityMensagensPortugues>()
						  .AddEntityFrameworkStores<Context>()
						  .AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Configurações da senha
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 0;


				// Configurações do usuario
				options.User.RequireUniqueEmail = false;
			});

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
