using Microsoft.EntityFrameworkCore;

namespace DDLDotNetCoreEF.Models
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Continent> Continent { get; set; }

    }
}
