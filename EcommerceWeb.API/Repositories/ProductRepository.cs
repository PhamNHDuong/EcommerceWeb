using EcommerceWeb.API.Interfaces;
using EcommerceWeb.Data.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace EcommerceWeb.API.Repositories
{
    public class ProductRepository : IProduct
    {
        private List<Product> products;

        public ProductRepository()
        {
            products = new List<Product>();
            new List<Product> {
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
                }.ForEach(p => AddProduct(p));
        }

        public Product this[string productName] => products.FirstOrDefault(p => p.Name == productName);

        public IEnumerable<Product> Products => throw new NotImplementedException();

        public Product AddProduct(Product product)
        {
            return product;
        }
    }
}
