using Microsoft.EntityFrameworkCore;
using PROG6212___CMCS___ST10082700.Models;

namespace PROG6212___CMCS___ST10082700.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClaimModel> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify the schema for Supabase
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<ClaimModel>(entity =>
            {
                entity.ToTable("Claims");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityAlwaysColumn(); // Specific for PostgreSQL
                entity.Property(e => e.LecturerUsername).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ClaimDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
