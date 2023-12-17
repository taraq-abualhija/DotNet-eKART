using Microsoft.EntityFrameworkCore;
using X_HIJA_SYSTEM.Models;

namespace X_HIJA_SYSTEM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CatID=100,CatName="UTP",DisplayOrder="1"},
                new Category { CatID=1000,CatName="Fiber",DisplayOrder="2"},
                new Category { CatID=12,CatName="UTPXS",DisplayOrder="3"}
            );
        }
    }
}
