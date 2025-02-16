using CV.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CV.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Debug.WriteLine("OnConfiguring was called but should be handled via DI.");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeder seeder = new Seeder();
            modelBuilder.Entity<Ibbi>().HasData(seeder.Ibbis);
        }


        public DbSet<Ibbi> Ibbis { get; set; }
    }
}
