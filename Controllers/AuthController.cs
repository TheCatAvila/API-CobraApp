using API_CobraApp.Application.Common.Responses;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(
            [FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(
                new LoginCommand(dto)
            );

            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(
                result,
                "Login successful"
            ));
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponse<object>>> ChangePassword(
    [FromBody] ChangePasswordDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _mediator.Send(
                new ChangePasswordCommand(userId, dto));

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Password changed successfully"
            ));
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ApiResponse<object>>> ForgotPassword(
            [FromBody] ForgotPasswordDto dto)
        {
            await _mediator.Send(new ForgotPasswordCommand(dto.Email));

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Si el correo existe, se enviará un código"
            ));
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse<object>>> ResetPassword(
            [FromBody] ResetPasswordDto dto)
        {
            await _mediator.Send(new ResetPasswordCommand(
                dto.Email,
                dto.Code,
                dto.NewPassword
            ));

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Contraseña actualizada correctamente"
            ));
        }
    }
}