using BaseNet.App.Auth.Users.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseNet.Api.Controllers.Auth.User
{
    [ApiController]
    [Route("api/auth/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }
    }
}