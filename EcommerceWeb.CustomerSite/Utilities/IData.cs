using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EcommerceWeb.CustomerSite.Utilities
{
    public interface IData
    {
        [Get("/Products/available")]
        Task<ViewListDto<ProductDto>> GetProductsAsync([FromQuery] int pageIndex);

        [Get("/Products/{id}")]
        Task<ProductDto> GetProductByIdAsync([FromRoute] Guid id);

        [Post("/Products/search")]
        Task<ViewListDto<ProductDto>> SearchingAsync(ProductSearchDto model);

        [Post("/Products/create")]
        Task<Response> CreateProduct(ProductSearchDto model);

        [Get("/Categories")]
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        [Post("/Users/register")]
        Task<Response> RegisterAsync(RegisterModel registerModel);

        [Post("/Users/login")]
        Task<Token> LoginAsync(LoginModel input);

        [Get("/Ratings")]
        Task<List<RatingDto>> GetRatingAsync(Guid id);

        [Post("/Ratings")]
        Task<ActionResult> CreateRatingAsync(RatingDto model/*, [Header("Authorization")] string bearerToken*/);

        [Get("/Images")]
        Task<List<ProductImageDto>> GetImages(Guid id);

    }
}
