using EcommerceWeb.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        //GET : Product
        public IActionResult Index()
        {
            IEnumerable<Product> products = null;
            return View();
        }
    }
}
