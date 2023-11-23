using Identity.Application.DTO;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Application
{
	public class OauthService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly IConfiguration _configuration;

		public OauthService(SignInManager<User> signInManager, IConfiguration configuration)
		{
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task<string> Login(UserLoginRequestDto request)
		{
			var result = await _signInManager.PasswordSignInAsync(request.Login, request.Password, false, true);
			return result.Succeeded ? ConstructJwt() : throw new Exception("usuario ou senha invalido");
		}

		private string ConstructJwt()
		{

			//jwk pode ser gerado pelo JWKSECDsaController
			var privateJwks = new JsonWebKey
			{
				Crv = _configuration["jwtKey:privateJwks:crv"],
				D = _configuration["jwtKey:privateJwks:d"],
				KeyId = _configuration["jwtKey:privateJwks:kid"],
				Kty = _configuration["jwtKey:privateJwks:kty"],
				X = _configuration["jwtKey:privateJwks:x"],
				Y = _configuration["jwtKey:privateJwks:y"],
			};

			var credentials = new SigningCredentials(privateJwks, SecurityAlgorithms.EcdsaSha256);
			var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()) };
			var token = new JwtSecurityToken("issuer", "audience", claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
