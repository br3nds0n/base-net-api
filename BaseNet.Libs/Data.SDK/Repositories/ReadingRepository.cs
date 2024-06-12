using System.Linq.Expressions;
using BaseNet.Libs.Data.SDK.Base;
using BaseNet.Libs.Data.SDK.Query;

namespace BaseNet.Libs.Data.SDK.Repositories
{
    public interface ReadingRepository<T> where T : EntityBase
    {
        Task<bool> Exist(Expression<Func<T, bool>> predicate);
        Task<T?> Read(Guid id);
        Task<T?> Read(Expression<Func<T, bool>> predicate, IEnumerable<string>? includes = null);
        Task<IEnumerable<T>> ReadAll(QueryOptions<T>? options = null);
        Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> predicate, QueryOptions<T>? options = null, IEnumerable<string>? includes = null);
    }
}