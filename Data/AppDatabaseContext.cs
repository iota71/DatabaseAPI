using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DatabaseAPI.Data
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<PersonelAccounts> PersonelAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonelAccounts>().HasKey(p => new { p.Name, p.Surname, p.TCKN, p.Email });
            base.OnModelCreating(modelBuilder);
        }
    }

    public class PersonelAccounts
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string TCKN { get; set; }

    }
}
