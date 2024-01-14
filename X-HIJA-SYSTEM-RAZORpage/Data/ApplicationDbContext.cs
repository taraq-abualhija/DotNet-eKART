using Microsoft.EntityFrameworkCore;
using X_HIJA_SYSTEM_RAZORpage.Models;

namespace X_HIJA_SYSTEM_RAZORpage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Categoryy> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoryy>().HasData(
                new Categoryy { CatID = 100, CatName = "UTP", DisplayOrder = 1 },
                new Categoryy { CatID = 1000, CatName = "Fiber", DisplayOrder = 3 },
                new Categoryy { CatID = 12, CatName = "UTPXS", DisplayOrder = 2 }
            );
        }
    }
}
