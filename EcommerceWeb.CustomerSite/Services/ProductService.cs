using EcommerceWeb.CustomerSite.Services.Interfaces;
using EcommerceWeb.CustomerSite.Utilities;
using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace EcommerceWeb.CustomerSite.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Product>> GetProduct()
        {
            //using (var client = _clientFactory.CreateClient())
            //{
            //    var request = new HttpRequestMessage(HttpMethod.Get, UrlRequest.GET_URL_PRODUCT());
            //    var response = await client.SendAsync(request);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await client.GetApiAsync<List<Product>>("Products");
            //        return data;
            //    }

            //    return null;
            //}
            var httpclient = _clientFactory.CreateClient();
            var data = await httpclient.GetApiAsync<List<Product>>("Products");

            if (data == null)
            {
                data = new List<Product>();
            }
            return data;
        }
        public async Task<ViewListDto<ProductDto>> GetProductsAsync([FromQuery] int pageIndex)
        {
            var httpclient = _clientFactory.CreateClient();
            var data = await httpclient.GetApiAsync<ViewListDto<ProductDto>>("Products/available");

            if (data == null)
            {
                data = new ViewListDto<ProductDto>();
            }
            return data;
        }
    }
}
