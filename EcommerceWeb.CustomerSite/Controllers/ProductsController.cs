using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EcommerceWeb.CustomerSite.Utilities;
using Microsoft.AspNetCore.Authorization;
using EcommerceWeb.Dto.Models;
using Refit;

namespace EcommerceWeb.CustomerSite.Controllers
{
    [AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly IData _data;

        public ProductsController(IData data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index([FromQuery] int pageIndex = 1)
        {
            var productsList = await _data.GetProductsAsync(pageIndex);
            ViewBag.categories = new SelectList(await _data.GetCategoriesAsync(), "CategoryId", "Name", null);
            List<ProductImageDto> imgs = new List<ProductImageDto>();
            foreach (var product in productsList.ModelDatas)
            {
                imgs.AddRange(_data.GetImages(product.ProductId).Result);
            }
            ViewData["ProductImages"] = imgs;
            return View(productsList);
        }

        public async Task<IActionResult> Search(string searchName, Guid searchType, [FromQuery] int pageIndex = 1)
        {
            var productsList = new ViewListDto<ProductViewDto>();
            try
            {
                if (searchType.Equals(Guid.Empty) && string.IsNullOrEmpty(searchName))
                {
                    return RedirectToAction("Index", new { pageIndex = 1 });
                }
                else if (string.IsNullOrEmpty(searchName))
                {
                    productsList = await _data.SearchingAsync(new ProductSearchDto { CategoryId = searchType, ProductName = "", PageIndex = pageIndex });
                }
                else
                {
                    productsList = await _data.SearchingAsync(new ProductSearchDto { CategoryId = searchType, ProductName = searchName, PageIndex = pageIndex });
                }
            }
            catch (ApiException e)
            {
                var errorList = await e.GetContentAsAsync<Dictionary<string, string>>();
                TempData["Error"] = errorList.First().Value;
                return RedirectToAction("Index", "Products", new { pageIndex = 1 });
            }

            var categoryList = await _data.GetCategoriesAsync();
            ViewData["Category"] = new SelectList(categoryList, "CategoryId", "Name");
            
            List<ProductImageDto> imgs = new List<ProductImageDto>();
            foreach (var product in productsList.ModelDatas)
            {
                imgs.AddRange(_data.GetImages(product.ProductId).Result);
            }

            ViewData["ProductImages"] = imgs;
            ViewData["SearchName"] = searchName;
            ViewData["SearchType"] = searchType;
            return View(productsList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id.Equals(Guid.Empty))
            {
                return NotFound();
            }

            var product = await _data.GetProductByIdAsync(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }

            List<ProductImageDto> imgs = new List<ProductImageDto>();
            imgs.AddRange(_data.GetImages(product.ProductId).Result);
            ViewData["ProductImages"] = imgs;

            try
            {
                var userId = User.Claims.FirstOrDefault(u => u.Type == "userid").Value;
                ViewData["userid"] = userId;

            }
            catch { }

            var ratingList = await _data.GetRatingAsync(id.GetValueOrDefault());
            ViewData["ratingList"] = ratingList;
            return View(product);
        }
    }
}
