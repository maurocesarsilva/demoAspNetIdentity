using Microsoft.OpenApi.Models;

namespace Identity.Api.Extensions
{
	public static class SwaggerExtension
	{
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen();

			services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
						  {
							  Reference = new OpenApiReference
							  {
								  Type = ReferenceType.SecurityScheme,
								  Id = "Bearer"
							  }
						  },
						 new string[] {}
					}
				});

			});

		}
	}
}
