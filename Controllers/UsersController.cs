using API_CobraApp.Application.Dtos.Users;
using API_CobraApp.Application.Features.Users.Create;
using API_CobraApp.Application.Features.Users.Delete;
using API_CobraApp.Application.Features.Users.GetAll;
using API_CobraApp.Application.Features.Users.GetById;
using API_CobraApp.Application.Features.Users.Patch;
using API_CobraApp.Application.Features.Users.Update;
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

        [Authorize]
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

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var result = await _mediator.Send(
                new UpdateUserCommand(id, dto));

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserDto>> Patch(int id, [FromBody] PatchUserDto dto)
        {
            var result = await _mediator.Send(
                new PatchUserCommand(id, dto));

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent(); // 204
        }

}
}