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
            modelBuilder.Entity<Education>()
             .HasOne(e => e.Ibbi)
             .WithMany(i => i.Educations)
             .HasForeignKey(e => e.IbbiId)
             .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>()
                .HasOne(i => i.Ibbi)
                .WithMany (x => x.Projects)
                .HasForeignKey(x => x.IbbiId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Language>()
                .HasOne(i => i.Ibbi)
                .WithMany(x => x.Languages)
                .HasForeignKey(k => k.IbbiId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ResumeExperience>()
                .HasOne( i => i.Ibbi)
                .WithMany(x => x.resumeExperiences)
                .HasForeignKey(fk => fk.IbbiId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Social>()
                .HasOne(i => i.Ibbi)
                .WithMany( i => i.Socials)
                .HasForeignKey(i => i.IbbiId)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<Ibbi> Ibbis { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ResumeExperience> ResumeExperiences { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Education> Educations { get; set; }
    }
}
