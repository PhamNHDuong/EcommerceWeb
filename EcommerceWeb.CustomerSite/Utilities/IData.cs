using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EcommerceWeb.CustomerSite.Utilities
{
    public interface IData
    {
        [Get("/Products/available")]
        Task<ViewListDto<ProductViewDto>> GetProductsAsync([FromQuery] int pageIndex);

        [Get("/Products/{id}")]
        Task<ProductViewDto> GetProductByIdAsync([FromRoute] Guid id);

        [Post("/Products/search")]
        Task<ViewListDto<ProductViewDto>> SearchingAsync(ProductSearchDto model);

        [Get("/Categories/available")]
        Task<IEnumerable<CategoryListDto>> GetCategoriesAsync();

        [Post("/Users/register")]
        Task<Response> RegisterAsync(RegisterModel registerModel);

        [Post("/Users/login")]
        Task<Token> LoginAsync(LoginModel input);

        [Get("/Ratings")]
        Task<List<RatingDto>> GetRatingAsync(Guid id);

        [Post("/Ratings")]
        Task<ActionResult> CreateRatingAsync(RatingDto model/*, [Header("Authorization")] string bearerToken*/);

        [Get("/Images/{id}")]
        Task<List<ProductImageDto>> GetImages(Guid id);

    }
}
