using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Client.Models;
using System.Diagnostics;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace PaymentMicroservice.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var apiBaseUrl = "https://localhost:7210/api/Auth"; // URL de la API externa
            var credentials = new { userName = model.UserName, password = model.Password };

            using (var client = new HttpClient())
            {
                // Envía las credenciales a la API externa
                var response = await client.PostAsJsonAsync(apiBaseUrl, credentials);

                if (response.IsSuccessStatusCode)
                {
                    // Obtiene el token JWT desde la API externa
                    var jwt = await response.Content.ReadAsStringAsync();

                    // Almacena el token JWT en las cookies
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName)); // Agrega otras claims según sea necesario
                    identity.AddClaim(new Claim("token", jwt)); // Agrega otras claims según sea necesario
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    if (TempData.ContainsKey("ReturnUrl"))
                    {
                        string returnUrl = TempData["ReturnUrl"].ToString();
                        TempData.Remove("ReturnUrl");

                        return LocalRedirect(returnUrl);
                    }
                    else { return RedirectToAction("Index"); }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error de inicio de sesión");
                    return View(model);
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
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