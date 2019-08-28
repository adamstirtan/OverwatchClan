using Microsoft.EntityFrameworkCore;

using Clan.ObjectModel;

namespace Clan.Web.Database.Repositories
{
    public sealed class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        { }
    }
}