using EcommerceWeb.CustomerSite.Interfaces;
using EcommerceWeb.Dto.Models;

namespace EcommerceWeb.CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<List<ProductDto>> GetAllAsyn()
        {
            var httpClient = _httpClientFactory.CreateClient();
            //var data = await httpClient.GetAsync()
            throw new NotImplementedException();
        }
    }
}
