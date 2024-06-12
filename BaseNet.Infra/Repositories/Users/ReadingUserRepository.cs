using BaseNet.Domain.Entities.User;
using BaseNet.Infra.Contexts;
using BaseNet.Libs.Data.SDK.Base;

namespace BaseNet.Infra.Repositories.Users
{
    public class ReadingUserRepository : ReadingRepositoryBase<User, ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;

        public ReadingUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}