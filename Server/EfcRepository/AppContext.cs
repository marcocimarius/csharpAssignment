using Microsoft.EntityFrameworkCore;

namespace EfcRepository;

public class AppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = app.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfcUser>().HasKey(x => x.Id);
        modelBuilder.Entity<EfcComment>().HasKey(x => x.Id);
        modelBuilder.Entity<EfcPost>().HasKey(x => x.Id);
    }
}