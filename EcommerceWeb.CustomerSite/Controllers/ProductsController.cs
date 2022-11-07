using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWeb.Data.DatabaseContext;
using EcommerceWeb.Data.Entities;
using Newtonsoft.Json;
using EcommerceWeb.CustomerSite.Utilities;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;
using EcommerceWeb.CustomerSite.Services.Interfaces;
using EcommerceWeb.Dto.Models;
using Refit;
using Microsoft.TeamFoundation.Common;

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
            return View(productsList);
        }

        public async Task<IActionResult> Search(string searchName, Guid searchType, [FromQuery] int pageIndex = 1)
        {
            var productsList = new ViewListDto<ProductDto>();
            try
            {
                if (searchType.Equals(Guid.Empty) && string.IsNullOrEmpty(searchName))
                {
                    return RedirectToAction("Index", new { pageIndex = 1 });
                }
                else if (string.IsNullOrEmpty(searchName))
                {
                    productsList = await _data.SearchingAsync(new RequestSearchProductDTO { CategoryId = searchType, ProductName = "", PageIndex = pageIndex });
                }
                else
                {
                    productsList = await _data.SearchingAsync(new RequestSearchProductDTO { CategoryId = searchType, ProductName = searchName, PageIndex = pageIndex });
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
            var cateName = product.CategoryName;
            if (product == null)
            {
                return NotFound();
            }

            //if (User.Identity.IsAuthenticated)
            //{
            //    var userId = User.Claims.FirstOrDefault(u => u.Type == "userid").Value;
            //    ViewData["userid"] = userId;
            //}
            else
            {
                ViewData["userid"] = null;
            }

            var ratingList = await _data.GetRatingAsync(id.GetValueOrDefault());
            ViewData["ratingList"] = ratingList;
            return View(product);
        }
    }
}
