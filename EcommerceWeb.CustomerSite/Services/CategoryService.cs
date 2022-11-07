using EcommerceWeb.CustomerSite.Services.Interfaces;
using EcommerceWeb.CustomerSite.Utilities;
using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.CustomerSite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var httpclient = _clientFactory.CreateClient();
            var data = await httpclient.GetApiAsync<IEnumerable<CategoryDto>>("Categories");

            if(data == null)
            {
                data = new List<CategoryDto>();
            }
            return data;
        }
    }
}
