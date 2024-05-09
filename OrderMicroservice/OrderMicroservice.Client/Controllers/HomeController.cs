using Microsoft.AspNetCore.Mvc;

namespace OrderMicroservice.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Orders");
        }
    }
}
