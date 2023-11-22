using Identity.Domain;

namespace Identity.Application.DTO
{
	public record UserRequestDto(string Login, string Password)
	{

		public User ToEntity()
		{
			return new User { UserName = Login };
		}
	}
}
