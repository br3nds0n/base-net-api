using System.Linq.Expressions;
using BaseNet.Libs.Data.SDK.Query;
using BaseNet.Libs.Data.SDK.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BaseNet.Libs.Data.SDK.Base
{
    public abstract class ReadingRepositoryBase<T, TDbContext> : ReadingRepository<T>
        where T : EntityBase where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        protected ReadingRepositoryBase(TDbContext context)
        {
            _context = context;
        }

        public Task<bool> Exist(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<T?> Read(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> Read(Expression<Func<T, bool>> predicate, IEnumerable<string>? includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        public Task<IEnumerable<T>> ReadAll(QueryOptions<T>? options)
        {
            return ReadAll(_ => true, options);
        }

        public async Task<IEnumerable<T>> ReadAll(
            Expression<Func<T, bool>> predicate,
            QueryOptions<T>? options = null,
            IEnumerable<string>? includes = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable().Where(predicate);

            if (options != null)
            {
                if (options.OrderBy != null)
                {
                    query = query.Ordenar(options.Order ?? QueryOrder.Asc, options.OrderBy);
                }

                if (options.Page != null && options.Limit != null)
                {
                    query = query.Paginar(options);
                }
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }
    }
}