using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.CustomerSite.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
