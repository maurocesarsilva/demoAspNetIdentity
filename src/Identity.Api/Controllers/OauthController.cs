using Identity.Application.DTO;
using Identity.Application;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
	[ApiController]
	[Route("oauth")]
	public class OauthController : ControllerBase
	{
		private readonly OauthService _oauthService;

		public OauthController(OauthService oauthService)
		{
			_oauthService = oauthService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request) => Ok(await _oauthService.Login(request));
	}
}
