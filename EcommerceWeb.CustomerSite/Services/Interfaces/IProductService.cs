using EcommerceWeb.Data.Entities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.CustomerSite.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProduct();
        Task<ViewListDto<ProductDto>> GetProductsAsync([FromQuery] int pageIndex);
        //Task<ProductDetailReadDto> GetProductDetailData(int id);
        //Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size);
        //Task<ProductListReadDto> GetFeaturedProductData(int page, int size);
        //Task<IEnumerable<ProductReadDto>> GetRelativeProductData(int id, int size);
        //Task<int> ProductRating(ProductRatingWriteDto dto);
    }
}
