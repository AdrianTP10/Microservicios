using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductMicroservice.Client.Models;
using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ProductMicroservice.Client.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string urlGetAll = "https://localhost:7240/Product";
        private string urlAdd = "https://localhost:7240/Product/AddProduct";
        private string urlGetById = "https://localhost:7240/Product/GetById";
        private string urlEdit = "https://localhost:7240/Product/EditProduct";
        private string urlDelete = "https://localhost:7240/Product/DeleteProduct";

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            List<Product> productList = new List<Product>();
            List<Product> productFilter = new List<Product>();
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Obtiene la lista
            productList = await client.GetFromJsonAsync<List<Product>>(urlGetAll);
            //Crea el modeo y se la pasa
            ProductModelViewModel viewModel = new ProductModelViewModel();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                productFilter = productList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                viewModel.Products = productFilter;
            }
            else
            {
                viewModel.Products = productList;
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
        public async Task<IActionResult> Create(ProductDTO productdto)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            //Serializa el producto
            string productSerializer = JsonConvert.SerializeObject(productdto);
            var content = new StringContent(productSerializer, Encoding.UTF8, "application/json");
            //Leer el resultado
            var response = await client.PostAsync(urlAdd, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Create content: "+ responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlAdd + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int productId)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Obtiene el producto
            Product product = await client.GetFromJsonAsync<Product>(urlGetById + "?productId=" + productId);
            //Verifica si esta nulo o no
            if (product == null) 
            {
                Console.WriteLine("Product not found wit the id: " + productId);
                return RedirectToAction("Index");
            }
            //Retorna al edit si tiene algo
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Serializa el producto
            string productSerializer = JsonConvert.SerializeObject(product);
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

        public async Task<IActionResult> Delete(int productId)
        {
            //Crea el httpclient
            var client = _httpClientFactory.CreateClient("WebApi");
            //Manda eliminar el producto
            var response = await client.DeleteAsync(urlDelete + "?productId=" + productId);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Delete content: " + responseContent);
            }
            else
            {
                Console.WriteLine("There was an error accessing the url: " + urlDelete + "?productId=" + productId + "\nRequestMessage: " + response.StatusCode);
            }
            //Regresa al index
            return RedirectToAction("Index");
        }
    }
}
