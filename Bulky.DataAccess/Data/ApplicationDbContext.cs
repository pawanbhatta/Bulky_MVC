using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action",
                    DisplayOrder = 1
                }, new Category
                {
                    Id = 2,
                    Name = "SciFi",
                    DisplayOrder = 2
                }, new Category
                {
                    Id = 3,
                    Name = "Romance",
                    DisplayOrder = 3
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Harry Porter",
                    Author = "Pawan Bhatta",
                    Description = "Good Movie",
                    ISBN = "SWD4644",
                    Price = 100,
                    Price50 = 400,
                    Price100 = 900
                }, new Product
                {
                    Id = 2,
                    Title = "John Wick",
                    Author = "Keanu Reeves",
                    Description = "Good Movie",
                    ISBN = "SWD4644",
                    Price = 150,
                    Price50 = 800,
                    Price100 = 9900
                }, new Product
                {
                    Id = 3,
                    Title = "The Wolverine",
                    Author = "Pawan Bhatta",
                    Description = "Good Movie",
                    ISBN = "SWD4644",
                    Price = 100,
                    Price50 = 400,
                    Price100 = 900
                }
                );
        }
    }
}
