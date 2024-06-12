using BaseNet.Domain.Entities.User;
using BaseNet.Infra.Contexts;
using BaseNet.Libs.Data.SDK.Base;
using BaseNet.Libs.Data.SDK.Repositories;

namespace BaseNet.Infra.Repositories
{
    public class AppUnitOfWork : UnitOfWorkBase<ApplicationDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(
            ApplicationDbContext context,
            WritingRepository<User> userRepository
        ) : base(context)
        {
            Users = userRepository;
        }

        public WritingRepository<User> Users { get; }
    }
}