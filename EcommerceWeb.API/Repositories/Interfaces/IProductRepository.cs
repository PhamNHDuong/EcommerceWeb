using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.API.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
