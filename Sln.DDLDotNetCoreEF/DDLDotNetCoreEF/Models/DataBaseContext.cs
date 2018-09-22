using Microsoft.EntityFrameworkCore;
using DDLDotNetCoreEF.Models;
using DDLDotNetCoreEF.Models.ViewModel;

namespace DDLDotNetCoreEF.Models
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Continent> Continent { get; set; }
        public DbSet<GlobalCitizen> GlobalCitizen { get; set; }
        //public DbSet<GlobalCitizenVM> GlobalCitizenVM { get; set; }
    }
}
