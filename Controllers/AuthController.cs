using API_CobraApp.Application.Dtos.Auth;
using API_CobraApp.Application.Features.Auth.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_CobraApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(
            [FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(
                new LoginCommand(dto)
            );

            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _mediator.Send(
                new ChangePasswordCommand(userId, dto));

            return NoContent(); // 204
        }
    }
}