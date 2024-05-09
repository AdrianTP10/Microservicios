using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UserMicroservice.Client.Models;

namespace UserMicroservice.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:7016/api/Users";

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var response = await _httpClient.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(content);
                return View(users);
            }
            else
            {
                
                return View("Error");
            }

        }


        // GET: UsersController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] User user)
        {
            try
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
                request.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                // sending the request..
                var response = await _httpClient.SendAsync(request);
 
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
 
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(content);
                return View(user);
            }
            else
            {

                return View("Error");
            }
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] User user)
        {
            try
            {
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                var request = new HttpRequestMessage(HttpMethod.Put, ApiUrl);
                request.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                // sending the request..
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"{ApiUrl}/{id}");

                // sending the request..
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
