using BaseNet.Infra.Contexts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BaseNet.App.Home.Queries.HomeQuery
{
    public class HomeQueryHandler : IRequestHandler<HomeQuery, HomeDTO>
    {
        private readonly ApplicationDbContext _context;
        public HomeQueryHandler(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<HomeDTO> Handle(HomeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _context.Database.EnsureCreated();

                return Task.FromResult(new HomeDTO
                {
                    App = "BaseNet.API",
                    Status = "Iniciada com sucesso",
                    Database = "OK"
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new HomeDTO
                {
                    App = "BaseNet.API",
                    Status = $"Erro ao iniciar: {ex.Message}",
                    Database = "ERROR"
                });
            }
        }
    }
}