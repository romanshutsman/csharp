using dbConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dbConnection.Data 
{
    public class DataContextEF : DbContext {
        public DbSet<Computer>?  Computer { get; set; }
        private IConfiguration _config;
        private string _connectionString;
        public DataContextEF(IConfiguration config) {
            _config = config;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            if(!options.IsConfigured) {
                options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase1;TrustServerCertificate=true;Trusted_Connection=false;User id=sa;Password=SQLConnect1!;",
                options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Computer>().ToTable("Computer", "TutorialAppSchema").HasKey(computer => computer.ComputerId);
        }
    }
}