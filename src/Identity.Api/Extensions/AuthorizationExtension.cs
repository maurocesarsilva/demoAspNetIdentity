using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Extensions
{
    public static class AuthorizationExtension
    {
        public static void AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var publicKey = new JsonWebKey
            {
                Crv = configuration["jwtKey:publicJwks:crv"],
                Kty = configuration["jwtKey:publicJwks:kty"],
                X = configuration["jwtKey:publicJwks:x"],
                Y = configuration["jwtKey:publicJwks:y"],
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = publicKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
