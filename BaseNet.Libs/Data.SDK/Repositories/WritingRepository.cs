using BaseNet.Libs.Data.SDK.Base;

namespace BaseNet.Libs.Data.SDK.Repositories
{
    public interface WritingRepository<T> where T : EntityBase
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}