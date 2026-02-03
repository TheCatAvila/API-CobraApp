using API_CobraApp.Application.Dtos.Auth;
using API_CobraApp.Application.Features.Auth.ChangePassword;
using API_CobraApp.Application.Features.Auth.ForgotPassword;
using API_CobraApp.Application.Features.Auth.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
        [FromBody] ForgotPasswordDto dto)
        {
            await _mediator.Send(new ForgotPasswordCommand(dto.Email));

            return Ok(new
            {
                message = "Si el correo existe, se enviará un código"
            });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordDto dto)
        {
            await _mediator.Send(new ResetPasswordCommand(
                dto.Email,
                dto.Code,
                dto.NewPassword
            ));

            return Ok(new
            {
                message = "Contraseña actualizada correctamente"
            });
        }
    }
}