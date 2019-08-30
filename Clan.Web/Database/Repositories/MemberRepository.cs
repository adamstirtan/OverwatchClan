using Microsoft.EntityFrameworkCore;

using Clan.ObjectModel;

namespace Clan.Web.Database.Repositories
{
    public sealed class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(DbContext context)
            : base(context)
        { }
    }
}