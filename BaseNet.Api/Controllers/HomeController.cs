using BaseNet.Infra.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace BaseNet.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Home()
        {
            try
            {
                _context.Database.EnsureCreated();

                return Ok(new
                {
                    App = "IWantApp.API",
                    Status = "Iniciada com sucesso",
                    Database = "OK"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    App = "IWantApp.API",
                    Status = $"Erro ao iniciar: {ex.Message}",
                    Database = "ERROR"
                });
            }
        }
    }
}
