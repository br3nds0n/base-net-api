using BaseNet.Libs.Data.SDK.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BaseNet.Libs.Data.SDK.Base
{
    public abstract class WritingRepositoryBase<T, TDbContext> : WritingRepository<T>
    where T : EntityBase where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        protected WritingRepositoryBase(TDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            var result = await _context.AddAsync(entity);
            return result.Entity;
        }

        public Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T> Update(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }
    }
}