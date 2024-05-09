using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderMicroservice.Client.Models;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using OrderMicroservice.Core.Products;
using System.Net.Http.Headers;
using System.Text;

namespace OrderMicroservice.Client.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private string urlGetAll = "https://localhost:7024/Order";
        private string urlGetById = "https://localhost:7024/Order/GetById";
        private string urlAddClean = "https://localhost:7024/Order/AddOrderClean";
        private string urlAddWithProduct = "https://localhost:7024/Order/AddOrderWithProduct";
        private string urlAddWithUser = "https://localhost:7024/Order/AddOrderWithUser";
        private string urlAddWithUserWithCity = "https://localhost:7024/Order/AddOrderWithUserWithCity";
        private string urlAddComplete = "https://localhost:7024/Order/AddOrderComplete";
        private string urlEdit = "https://localhost:7024/Order/EditOrder";
        private string urlDelete = "https://localhost:7024/Order/DeleteOrder";

        public OrdersController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync(string searchTerm)
        {
            List<Order> orderList = new List<Order>();
            List<Order> ordersFilter = new List<Order>();
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Obtiene la lista
            orderList = await client.GetFromJsonAsync<List<Order>>(urlGetAll);
            //Crea el modelo y se la pasa
            OrderViewModel viewModel = new OrderViewModel();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ordersFilter = orderList.Where(x => x.Status.ToLower().Contains(searchTerm.ToLower())).ToList();
                viewModel.Orders = ordersFilter;
            }
            else
            {
                viewModel.Orders = orderList;
            }
            ViewBag.SearchTerm = searchTerm;
            //Retorna la vista
            return View(viewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderClean orderClean)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderClean);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAddClean, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create Normal Content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAddClean + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public IActionResult CreateWithProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWithProduct(OrderWithProduct orderWithProduct)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderWithProduct);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAddWithProduct, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create With Product Content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAddWithProduct + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public IActionResult CreateWithUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWithUser(OrderWithUser orderWithUser)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderWithUser);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAddWithUser, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create With User Content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAddWithUser + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public IActionResult CreateWithUserWithCity()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWithUserWithCity(OrderWithUserWithCity orderWithUserWithCity)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderWithUserWithCity);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAddWithUserWithCity, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create With User With City Content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAddWithUserWithCity + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public IActionResult CreateComplete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateComplete(OrderDTO orderdto)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderdto);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAddComplete, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create Complete Content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAddComplete + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int orderId)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Obtiene el order
            Order order = await client.GetFromJsonAsync<Order>(urlGetById + "?orderId=" + orderId);

            OrderClean orderClean= _mapper.Map<OrderClean>(order);
            //Verifica si esta nulo o no
            if (order == null)
            {
                Console.WriteLine("Order not found wit the id: " + orderId);
                return RedirectToAction("Index");
            }
            //Retorna al edit si tiene algo
            return View(orderClean);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderClean orderClean)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Serializa el order
            string productSerializer = JsonConvert.SerializeObject(orderClean);
            //Configura el cliente
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, urlEdit);
            request.Content = content;

            // Realiza la solicitud PUT
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Edit content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlEdit + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int orderId)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Manda eliminar el order
            var response = await client.DeleteAsync(urlDelete + "?orderId=" + orderId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Delete content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlDelete + "?orderId=" + orderId + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
    }
}
