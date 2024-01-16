using BaseNet.App.Commands.Exemplo;
using BaseNet.App.Queries.Exemplo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseNet.Api.Controllers
{
    [ApiController]
    [Route("api/exemplo")]
    public class ExemploController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExemploController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarExemplo([FromBody] ExemploDTO exemploDTO)
        {
            var command = new CriarExemploCommand(exemploDTO);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObterExemplo()
        {
            var query = new ObterExemploQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}