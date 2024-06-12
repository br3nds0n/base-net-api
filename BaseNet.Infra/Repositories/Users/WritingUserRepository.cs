using BaseNet.Domain.Entities.User;
using BaseNet.Infra.Contexts;
using BaseNet.Libs.Data.SDK.Base;

namespace BaseNet.Infra.Repositories.Users
{
    public class WritingUserRepository : WritingRepositoryBase<User, ApplicationDbContext>
    {
        public WritingUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}