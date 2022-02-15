using Microsoft.EntityFrameworkCore;
using TaskManagementService.Application.Models;

namespace TaskManagementService.Api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().Property( et => et.Id )
                .IsRequired()
                .ValueGeneratedNever();

            modelBuilder.Entity<TaskItem>().Property(et => et.Secret)
                .HasMaxLength(55)
                .HasDefaultValue("");
        }
        
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}