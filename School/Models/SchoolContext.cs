using Microsoft.EntityFrameworkCore;

namespace School.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public SchoolContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(_ => _.Id);
            modelBuilder.Entity<Student>().Property(_ => _.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
