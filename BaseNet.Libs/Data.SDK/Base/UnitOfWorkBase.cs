using BaseNet.Libs.Data.SDK.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BaseNet.Libs.Data.SDK.Base
{
    public abstract class UnitOfWorkBase<TDbContext> : UnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        protected UnitOfWorkBase(TDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            BeforeValidateCommit();
            var modificacoes = await _context.SaveChangesAsync();
            AfterValidatingCommit(modificacoes);
        }

        private void BeforeValidateCommit()
        {
            var modificacoes = _context.ChangeTracker.Entries()
                .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                .ToList();

            if (!modificacoes.Any())
                throw new Exception("Não há alterações para serem salvas no banco de dados");
        }

        private void AfterValidatingCommit(int quantidadeDeModificacoes)
        {
            if (quantidadeDeModificacoes <= 0)
                throw new Exception("Nenhuma alteração foi salva no banco de dados");
        }
    }
}