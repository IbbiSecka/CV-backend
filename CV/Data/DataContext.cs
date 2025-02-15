using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CV.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration): base (options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            optionsBuilder.UseNpgsql(connectionString);

            optionsBuilder.LogTo(message => Debug.WriteLine(message)); // Log SQL queries
        }
    }
}
