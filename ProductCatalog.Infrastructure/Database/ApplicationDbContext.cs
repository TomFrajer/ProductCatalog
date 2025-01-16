using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Define the Products DbSet
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed initial data
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", ImgUri = "https://example.com/img1.jpg", Price = 1000, Description = "First product" },
            new Product { Id = 2, Name = "Product 2", ImgUri = "https://example.com/img2.jpg", Price = 2000, Description = "Second product" }
        );
    }
}
