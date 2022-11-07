using EcommerceWeb.CustomerSite.Models;
using EcommerceWeb.CustomerSite.Services;
using EcommerceWeb.CustomerSite.Services.Interfaces;
using EcommerceWeb.CustomerSite.Utilities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceWeb.CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ProductListReadDto data = await _productService.GetFeaturedProductData(ConstantVariable.DEFAULT_PAGE_NUMBER, ConstantVariable.DEFAULT_SIZE_PER_PAGE);

            //if (data is null)
            //{
            //    return RedirectToAction("Index", "Error");
            //}

            //var vm = new HomeViewModel()
            //{
            //    Products = data.Products
            //};

            return View(/*vm*/);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}