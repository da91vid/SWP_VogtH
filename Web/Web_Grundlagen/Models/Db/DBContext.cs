using Microsoft.EntityFrameworkCore;

namespace Web_Grundlagen.Models.Db
{
    public class DBContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;database=Web_Grundlagen;user=root;password=david2003";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}