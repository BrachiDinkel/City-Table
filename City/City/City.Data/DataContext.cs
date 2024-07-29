using City.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace City.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CityClass> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityClass>()
                .HasKey(c => c.Name);

            modelBuilder.Entity<CityClass>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
