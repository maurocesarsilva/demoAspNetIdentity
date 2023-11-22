using Identity.Domain;

namespace Identity.Application.DTO
{
	public record UserResponseDto(string Id, string Login)
	{
		public static UserResponseDto ToDto(User user)
		{
			return new UserResponseDto(user.Id, user.UserName);
		}
	}
}
