using API_CobraApp.Application.Dtos.Users;
using API_CobraApp.Application.Features.Users.Create;
using API_CobraApp.Application.Features.Users.GetAll;
using API_CobraApp.Application.Features.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_CobraApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(
            [FromBody] CreateUserDto dto)
        {
            var result = await _mediator.Send(
                new CreateUserCommand(dto));

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user is null)
                return NotFound();

            return Ok(user);
        }
    }
}