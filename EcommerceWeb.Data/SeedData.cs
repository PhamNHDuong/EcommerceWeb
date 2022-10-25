using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceWeb.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Game2",
                        Description = "Test2",
                        Price = 2.6,
                        InStock = true,
                        Stock = 1,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    },

                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Game1",
                        Description = "Test",
                        Price = 1.6,
                        InStock = true,
                        Stock = 1,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    },

                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Game3",
                        Description = "Test3",
                        Price = 1.6,
                        InStock = true,
                        Stock = 1,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    },

                    new Product
                    {
                        ProductId = Guid.NewGuid(),
                        Name = "Game4",
                        Description = "Test4",
                        Price = 1.6,
                        InStock = true,
                        Stock = 1,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
