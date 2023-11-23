using Identity.Application.DTO;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Application
{
	public class UserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IConfiguration _configuration;

		public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		public async Task<UserResponseDto> Create(UserRequestDto request)
		{
			var entity = request.ToEntity();

			var result = await _userManager.CreateAsync(entity, request.Password);
			if (result.Succeeded is false) throw new Exception(string.Join(" | ", result.Errors.Select(s => s.Description)));


			return UserResponseDto.ToDto(entity);
		}

		public async Task<IEnumerable<UserResponseDto>> Get()
		{
			var users = await _userManager.Users.ToListAsync();

			return users.Select(x => UserResponseDto.ToDto(x));
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
			return  new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
