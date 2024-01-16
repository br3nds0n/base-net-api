using BaseNet.App.Commands.Exemplo;
using BaseNet.App.Queries.Exemplo;
using MediatR;

namespace BaseNet.App.Handlers.Exemplo
{
    public class ObterExemploQueryHandler : IRequestHandler<ObterExemploQuery, ExemploDTO>
    {
        public Task<ExemploDTO> Handle(ObterExemploQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ExemploDTO { Nome = "Exemplo" });
        }
    }
}