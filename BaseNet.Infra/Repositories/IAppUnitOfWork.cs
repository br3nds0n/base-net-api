using BaseNet.Domain.Entities.User;
using BaseNet.Libs.Data.SDK.Repositories;

namespace BaseNet.Infra.Repositories
{
    public interface IAppUnitOfWork : UnitOfWork
    {
        public WritingRepository<User> Users { get; }
    }
}