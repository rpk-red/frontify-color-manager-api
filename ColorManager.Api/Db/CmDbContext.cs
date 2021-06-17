using ColorManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorManager.Api.Db
{
    public class CmDbContext : DbContext
    {
        public CmDbContext(DbContextOptions<CmDbContext> options) : base(options) { }

        public DbSet<Color> ColorItems { get; set; }
    }
}
