using EcommerceWeb.API.Repositories.Interfaces;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.API.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await GetAll()
               .Include(p => p.Category)
               .OrderBy(p => p.Name)
               .ToListAsync();
        }

        public async Task<Product> GetOneProductsAsync(Guid productId)
        {
            return await GetAll()
                .Include(p => p.Category)
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}