using MediatR;
using Microsoft.AspNetCore.Mvc;

using BaseNet.App.Home.Queries.HomeQuery;

namespace BaseNet.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Home()
        {
            var result = _mediator.Send(new HomeQuery()).Result;
            return Ok(result);
        }
    }
}
