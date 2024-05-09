using ConsoleTest.Core.Orders;
using ConsoleTest.Core.Payments;
using ConsoleTest.Core.Products;
using ConsoleTest.Core.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Checker
    {

        static async Task Main(string[] args)
        {
            string urlProduct = "https://localhost:7240/Product";
            string urlProductConsult = "https://localhost:7240/Product/GetById";
            string urlOrder = "https://localhost:7024/Order";
            string urlUsers = "https://localhost:7016/api/Users";
            string urlPayment = "https://localhost:7210/api/Payments";
            string urlAuth = "https://localhost:7210/api/Auth";
            string email = "admin@payments.com";
            string password = "Pa$$word.1";
            
            Console.WriteLine("Calling the 'product' check with the url of:" + urlProduct);
            await CheckProducts(urlProduct);
            Console.WriteLine("\nCalling the 'users' check with the url of:" + urlUsers);
            await CheckUser(urlUsers);
            Console.WriteLine("\nCalling the 'order' check with the url of:" + urlOrder);
            await CheckOrder(urlOrder);
            Console.WriteLine("\nCalling the 'payment' check with the url of:" + urlPayment);
            bool authorize = await CheckPayment(urlPayment, "sdfsdfasfas");
            if (!authorize)
            {
                Console.WriteLine("\nThere were no permissions, trying to get the token with the following account: email: '" + email + "', password: '" + password + "'");
            }
            string token = await GetToken(urlAuth, email, password);
            Console.WriteLine("\nToken get: " + token);
            Console.WriteLine("\nCalling the 'payment' check with the url of:" + urlPayment);
            bool newAuthorize = await CheckPayment(urlPayment, token);

            
            int idUser = 0; // Declarar la variable fuera del if
            int idProduct = 0; // Declarar la variable fuera del if
            bool salida = true;
            while (salida)
            {
                Console.Write("\nPlease, enter the id user: ");

                string stringIdUser = Console.ReadLine();
                if (int.TryParse(stringIdUser, out idUser))
                {
                    if (idUser <= 0)
                    {
                        Console.WriteLine("The id user most be greater than 0");
                    }
                    else
                    {
                        salida = false;
                    }
                }
                else
                {
                    Console.WriteLine("The input is not a valid integer for id user.");
                }

            }
            salida = true;
            while (salida)
            {
                Console.Write("\nPlease, enter the id product: ");
                string stringIdProduct = Console.ReadLine();
                if (int.TryParse(stringIdProduct, out idProduct))
                {
                    if(idProduct <= 0)
                    {
                        Console.WriteLine("The id product most be greater than 0");
                    }
                    else
                    {
                        salida = false;
                    }
                }
                else
                {
                    Console.WriteLine("The input is not a valid integer for id product.");
                }

            }

            Console.WriteLine("\nValidating user ...");
            bool existUser = await ExistUser(urlUsers, idUser);
            if (existUser)
            {
                Console.WriteLine("The user with the id user: '" + idUser + "' exist!!");
            }
            else 
            { 
                Console.WriteLine("The user with the id user: '" + idUser + "' doesn´t exist :c");
            }
            Console.WriteLine("\nValidating product ...");
            bool existProduct = await ExistProduct(urlProductConsult, idProduct);
            if (existProduct)
            {
                Console.WriteLine("The product with the id product: '" + idProduct + "' exist!!");
            }
            else
            {
                Console.WriteLine("The product with the id product: '" + idProduct + "' doesn´t exist :c");
            }
        }

        static async Task CheckProducts(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if(content != null)
                    {
                        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);
                        if (products.Count == 0)
                        {
                            Console.WriteLine("List of empty products");
                        }
                        else {
                            foreach (var product in products)
                            {
                                Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Category: {product.Category}, Price: {product.Price}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url+"\nRequestMessage: "+response.RequestMessage);
                }
            }
        }

        static async Task CheckUser(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
                        if (users.Count == 0)
                        {
                            Console.WriteLine("List of empty users");
                        }
                        else
                        {
                            foreach (var user in users)
                            {
                                Console.WriteLine($"Id: {user.Id}, FirstName: {user.FirstName}, Role: {user.Role}, CityId: {user.CityId}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.RequestMessage);
                }
            }
        }

        static async Task CheckOrder(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(content);
                        if (orders.Count == 0)
                        {
                            Console.WriteLine("List of empty orders");
                        }
                        else
                        {
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Id: {order.Id}, OrderDate: {order.OrderDate}, Status: {order.Status}, ProductName: {order.Product.Name}, UserName: {order.User.FirstName}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.RequestMessage);
                }
            }
        }

        static async Task<bool> CheckPayment(string url, string token)
        {
            bool authorize = false;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        authorize = true;
                        List<Payment> payments = JsonConvert.DeserializeObject<List<Payment>>(content);
                        if (payments.Count == 0)
                        {
                            Console.WriteLine("List of empty payments");
                        }
                        else
                        {
                            foreach (var payment in payments)
                            {
                                Console.WriteLine($"Id: {payment.Id}, Amount: {payment.Amount}, OrderId: {payment.OrderId}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.StatusCode);
                }
            }
            return authorize;
        }

        static async Task<string> GetToken(string url, string email, string password)
        {
            string token = "";
            using (var httpClient = new HttpClient())
            {
                var user = new UserJWToken { userName = email, password = password };
                var response = await httpClient.PostAsJsonAsync(url, user);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        token = content;
                    }
                    else
                    {
                        Console.WriteLine("There was an error to get the token");
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.StatusCode);
                }
            }
            return token;
        }

        static async Task<bool> ExistProduct(string url, int id)
        {
            bool exist = false;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url+ "?productId=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        try
                        {
                            Product product = JsonConvert.DeserializeObject<Product>(content);
                            if (product != null)
                            {
                                exist = true;
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Error: {content}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.RequestMessage);
                }
            }
            return exist;
        }

        static async Task<bool> ExistUser(string url, int id)
        {
            bool exist = false;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url+ "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        try
                        {
                            User user = JsonConvert.DeserializeObject<User>(content);
                            if (user != null)
                            {
                                exist = true;
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Error: {content}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There was an error accessing the url: " + url + "\nRequestMessage: " + response.RequestMessage);
                }
            }
            return exist;
        }
    }
}
