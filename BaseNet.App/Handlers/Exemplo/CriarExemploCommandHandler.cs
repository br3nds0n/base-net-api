using BaseNet.App.Commands.Exemplo;
using MediatR;

namespace BaseNet.App.Handlers
{
    public class CriarExemploCommandHandler : IRequestHandler<CriarExemploCommand>
    {
        public async Task Handle(CriarExemploCommand request, CancellationToken cancellationToken) {
            Console.WriteLine(request.ExemploDTO.Nome);

            await Task.CompletedTask;
        }
    }
}