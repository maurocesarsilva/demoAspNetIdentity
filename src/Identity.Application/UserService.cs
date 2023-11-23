using Identity.Application.DTO;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application
{
	public class UserService
	{
		private readonly UserManager<User> _userManager;

		public UserService(UserManager<User> userManager)
		{
			_userManager = userManager;
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
	}
}
