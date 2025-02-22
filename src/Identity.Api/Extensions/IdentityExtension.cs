using Identity.Domain;
using Identity.Repository;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Extensions
{
    public static class IdentityExtension
    {
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuração do identity server
            services.AddDefaultIdentity<User>()
                        .AddRoles<IdentityRole>()
                        .AddErrorDescriber<IdentityMensagensPortugues>()
                        .AddEntityFrameworkStores<Context>()
                        .AddDefaultTokenProviders();
                        //.AddTokenProvider<CustomTokenProvider>("CustomTokenProvider"); custom provider

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
        }


    }
    public class CustomTokenProvider : IUserTwoFactorTokenProvider<User>
    {
        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            throw new NotImplementedException();
        }
    }
}
