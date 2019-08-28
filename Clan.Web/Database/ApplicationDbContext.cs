using Microsoft.EntityFrameworkCore;

using Clan.ObjectModel;

namespace Clan.Web.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}