using Identity.Application;
using Identity.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Identity.Api.Controllers
{
    [ApiController]
	[Route("user")]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(UserRequestDto request) => StatusCode((int)HttpStatusCode.Created, await _userService.Create(request));

		[HttpGet]
		public async Task<IActionResult> Get() => Ok(await _userService.Get());

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request) => Ok(await _userService.Login(request));
	}
}
