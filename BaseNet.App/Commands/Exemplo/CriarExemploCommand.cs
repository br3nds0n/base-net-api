using MediatR;

namespace BaseNet.App.Commands.Exemplo
{
    public class CriarExemploCommand : IRequest
    {
        public ExemploDTO ExemploDTO { get; set; }

        public CriarExemploCommand(ExemploDTO exemploDTO)
        {
            ExemploDTO = exemploDTO;
        }
    }
}