using API_CobraApp.Application.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}