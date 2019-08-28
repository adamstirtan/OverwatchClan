using Microsoft.EntityFrameworkCore;

using Clan.ObjectModel;

namespace Clan.Web.Database.Repositories
{
    public sealed class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(DbContext context)
            : base(context)
        { }
    }
}