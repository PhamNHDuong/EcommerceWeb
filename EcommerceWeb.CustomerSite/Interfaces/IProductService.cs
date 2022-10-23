using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.CustomerSite.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsyn();
    }
}
