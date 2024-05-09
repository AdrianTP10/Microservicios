using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentMicroservice.Client.Models;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace PaymentMicroservice.Client.Controllers
{

    [Authorize]
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:7210/api/Payments";

        public PaymentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: PaymentsController
        public async Task<ActionResult> IndexAsync()
        {
            var jwtToken = HttpContext.User.FindFirst("token")?.Value;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                //var response = await _httpClient.GetAsync(ApiUrl);

                List<PaymentViewModel> payments = await client.GetFromJsonAsync<List<PaymentViewModel>>(ApiUrl);
                return View(payments);
            }
        }


        // GET: PaymentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] PaymentViewModel payment)
        {
            var jwtToken = HttpContext.User.FindFirst("token")?.Value;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                    var viewModelJson = JsonConvert.SerializeObject(payment);
                    var content = new StringContent(viewModelJson, Encoding.UTF8, "application/json");

                    var createResponse = await client.PostAsync(ApiUrl, content);

                    if (createResponse.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Maneja errores o devuelve un valor predeterminado
                        return RedirectToAction("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        // GET: PaymentsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var jwtToken = HttpContext.User.FindFirst("token")?.Value;
            try { 
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                    var response = await client.GetAsync($"{ApiUrl}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var viewModel = JsonConvert.DeserializeObject<PaymentViewModel>(content);

                        return View(viewModel);
                    }
                    else
                    {
                        // Maneja errores o devuelve un valor predeterminado
                        return RedirectToAction("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        // POST: PaymentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] PaymentViewModel payment)
        {
            var jwtToken = HttpContext.User.FindFirst("token")?.Value;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var viewModelJson = JsonConvert.SerializeObject(payment);
                    var content = new StringContent(viewModelJson, Encoding.UTF8, "application/json");

                    // Realiza la solicitud PUT
                    var putResponse = await client.PutAsync(ApiUrl, content);

                    if (putResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        // GET: PaymentsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var jwtToken = HttpContext.User.FindFirst("token")?.Value;
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    var deleteResponse = await client.DeleteAsync($"{ApiUrl}/{id}");

                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
