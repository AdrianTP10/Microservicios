using Microsoft.AspNetCore.Mvc;

namespace ProductMicroservice.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Products");
        }
    }
}
