using dbConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace dbConnection.Data 
{
    public class DataContextEF : DbContext {
        public DbSet<Computer>?  Computer { get; set; }
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