//using EcommerceWeb.Data.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System.Xml.Linq;

//namespace EcommerceWeb.Data.DatabaseContext
//{
//    public class SeedData
//    {
//        public static void Initialize(IServiceProvider serviceProvider)
//        {
//            using (var context = new ApplicationDbContext(
//                serviceProvider.GetRequiredService<
//                    DbContextOptions<ApplicationDbContext>>()))
//            {
//                // Look for any movies.
//                if (context.Products.Any())
//                {
//                    return;   // DB has been seeded
//                }
//                context.Categories.AddRange(
//                    new Category
//                    {
//                        CategoryId = Guid.NewGuid(),
//                        Name = "Action",
//                        DateCreated = DateTime.Now,
//                        DateUpdated = DateTime.Now,
//                        Products = new List<Product>
//                        {
//                            new Product
//                            {
//                                ProductId = Guid.NewGuid(),
//                                Name = "Game2",
//                                Description = "Test2",
//                                Price = 2.6,
//                                InStock = true,
//                                Stock = 1,
//                                IsDeleted = false,
//                                DateCreated = DateTime.Now,
//                                DateUpdated = DateTime.Now,
//                            },
//                            new Product
//                            {
//                                ProductId = Guid.NewGuid(),
//                                Name = "Game1",
//                                Description = "Test",
//                                Price = 1.6,
//                                InStock = true,
//                                Stock = 1,
//                                IsDeleted = false,
//                                DateCreated = DateTime.Now,
//                                DateUpdated = DateTime.Now,
//                            },
//                        },
//                    },
//                    new Category
//                    {
//                        CategoryId = Guid.NewGuid(),
//                        Name = "MMO",
//                        DateCreated = DateTime.Now,
//                        DateUpdated = DateTime.Now,
//                        Products = new List<Product>
//                        {
//                            new Product
//                            {
//                                ProductId = Guid.NewGuid(),
//                                Name = "Game3",
//                                Description = "Test3",
//                                Price = 1.6,
//                                InStock = true,
//                                Stock = 1,
//                                IsDeleted = false,
//                                DateCreated = DateTime.Now,
//                                DateUpdated = DateTime.Now,
//                            },
//                            new Product
//                            {
//                                ProductId = Guid.NewGuid(),
//                                Name = "Game4",
//                                Description = "Test4",
//                                Price = 1.6,
//                                InStock = true,
//                                Stock = 1,
//                                IsDeleted = false,
//                                DateCreated = DateTime.Now,
//                                DateUpdated = DateTime.Now,
//                            }
//                        }
//                    }
//                    );
//                context.SaveChanges();
//            }
//        }
//    }
//}
