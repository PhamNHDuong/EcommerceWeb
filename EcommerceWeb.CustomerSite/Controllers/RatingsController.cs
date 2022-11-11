using EcommerceWeb.CustomerSite.Utilities;
using EcommerceWeb.Dto.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IData _data;

        public RatingsController(IData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AUserAUserId,ProductProductId,Star,Comment")] RatingDto rating)
        {
            //if (ModelState.IsValid)
            var token = User.Claims.FirstOrDefault(u => u.Type == "token").Value;
            
            try
            {
                var response = await _data.CreateRatingAsync(rating);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("An error occured deserializing the response."))
                {
                    TempData["Message"] = "Comment success";
                    return RedirectToAction("Details", "Products", new { id = rating.ProductProductId });
                }
                TempData["Error"] = "Comment fail, please check details";
                return RedirectToAction("Details", "Products", new { id = rating.ProductProductId });
            }
            return RedirectToAction("Details", "Products", new { id = rating.ProductProductId });
        }
    }
}
