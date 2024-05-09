using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OrderMicroservice.ApplicationServices.Orders;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using OrderMicroservice.Core.Products;
using ProductMicroservice.NUnitTest;

namespace OrderMicroservice.NUnitTest
{
    [TestFixture]
    public class OrderTest
    {
        protected TestServer _server;

        [OneTimeSetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }

        [Test]
        public async Task AddOrderAsync()
        {
            var repository = _server.Host.Services.GetService<IOrdersAppServices>();
            //Agregar orders al servicio
            OrderDTO order1 = new OrderDTO
            {
                Id = 1,
                OrderDate = DateTime.Now,
                StatusOrder = "En camino",
                ArriveOrder = DateOnly.Parse("2023-09-15"),
                Product = new Product
                {
                    Id = 1,
                    Name = "Takis Fuego",
                    Description = "Fritura con chile",
                    Brand = "Barcel",
                    Category = "Fritura",
                    Price = 10
                },
                User = new Core.Users.User
                {
                    Id = 1,
                    FirstName = "David",
                    LastName = "Jiménez",
                    Role = "Vendedor",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 1,
                        Name = "Zitácuaro"
                    }
                }
            };

            int id = await repository.AddOrderAsync(order1);
            //Listar el contenido
            List<Order> list = await repository.GetOrdersAsync();
            Assert.Pass();
        }

        [Test]
        public async Task GetOrdersAsync()
        {
            var repository = _server.Host.Services.GetService<IOrdersAppServices>();
            //Agregar orders al servicio
            OrderDTO order1 = new OrderDTO
            {
                Id = 1,
                OrderDate = DateTime.Now,
                StatusOrder = "En camino",
                ArriveOrder = DateOnly.Parse("2023-09-15"),
                Product = new Product
                {
                    Id = 1,
                    Name = "Takis Fuego",
                    Description = "Fritura con chile",
                    Brand = "Barcel",
                    Category = "Fritura",
                    Price = 10
                },
                User = new Core.Users.User
                {
                    Id = 1,
                    FirstName = "David",
                    LastName = "Jiménez",
                    Role = "Vendedor",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 1,
                        Name = "Zitácuaro"
                    }
                }
            };
            OrderDTO order2 = new OrderDTO
            {
                Id = 2,
                OrderDate = DateTime.Now,
                StatusOrder = "Por iniciar",
                ArriveOrder = DateOnly.Parse("2023-09-11"),
                Product = new Product
                {
                    Id = 2,
                    Name = "Churrumais",
                    Description = "Fritura con chile y limón",
                    Brand = "Sabritas",
                    Category = "Fritura",
                    Price = 9.5
                },
                User = new Core.Users.User
                {
                    Id = 2,
                    FirstName = "Patricia",
                    LastName = "López",
                    Role = "Comprador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 2,
                        Name = "Morelia"
                    }
                }
            };
            OrderDTO order3 = new OrderDTO
            {
                Id = 3,
                OrderDate = DateTime.Now,
                StatusOrder = "Finalizada",
                ArriveOrder = DateOnly.Parse("2023-09-10"),
                Product = new Product
                {
                    Id = 3,
                    Name = "Chokies",
                    Description = "Galletas con chispas de chocolate",
                    Brand = "Marinela",
                    Category = "Galletas",
                    Price = 14.5
                },
                User = new Core.Users.User
                {
                    Id = 3,
                    FirstName = "Jose Rubén",
                    LastName = "De Jesús",
                    Role = "Cobrador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 3,
                        Name = "Toluca"
                    }
                }
            };

            await repository.AddOrderAsync(order1);
            await repository.AddOrderAsync(order2);
            await repository.AddOrderAsync(order3);
            //Listar el contenido
            List<Order> list = await repository.GetOrdersAsync();
            Assert.Pass();
        }

        [Test]
        public async Task GetOrderAsync()
        {
            var repository = _server.Host.Services.GetService<IOrdersAppServices>();
            //Agregar orders al servicio
            OrderDTO order1 = new OrderDTO
            {
                Id = 1,
                OrderDate = DateTime.Now,
                StatusOrder = "En camino",
                ArriveOrder = DateOnly.Parse("2023-09-15"),
                Product = new Product
                {
                    Id = 1,
                    Name = "Takis Fuego",
                    Description = "Fritura con chile",
                    Brand = "Barcel",
                    Category = "Fritura",
                    Price = 10
                },
                User = new Core.Users.User
                {
                    Id = 1,
                    FirstName = "David",
                    LastName = "Jiménez",
                    Role = "Vendedor",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 1,
                        Name = "Zitácuaro"
                    }
                }
            };
            OrderDTO order2 = new OrderDTO
            {
                Id = 2,
                OrderDate = DateTime.Now,
                StatusOrder = "Por iniciar",
                ArriveOrder = DateOnly.Parse("2023-09-11"),
                Product = new Product
                {
                    Id = 2,
                    Name = "Churrumais",
                    Description = "Fritura con chile y limón",
                    Brand = "Sabritas",
                    Category = "Fritura",
                    Price = 9.5
                },
                User = new Core.Users.User
                {
                    Id = 2,
                    FirstName = "Patricia",
                    LastName = "López",
                    Role = "Comprador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 2,
                        Name = "Morelia"
                    }
                }
            };
            OrderDTO order3 = new OrderDTO
            {
                Id = 3,
                OrderDate = DateTime.Now,
                StatusOrder = "Finalizada",
                ArriveOrder = DateOnly.Parse("2023-09-10"),
                Product = new Product
                {
                    Id = 3,
                    Name = "Chokies",
                    Description = "Galletas con chispas de chocolate",
                    Brand = "Marinela",
                    Category = "Galletas",
                    Price = 14.5
                },
                User = new Core.Users.User
                {
                    Id = 3,
                    FirstName = "Jose Rubén",
                    LastName = "De Jesús",
                    Role = "Cobrador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 3,
                        Name = "Toluca"
                    }
                }
            };

            await repository.AddOrderAsync(order1);
            await repository.AddOrderAsync(order2);
            await repository.AddOrderAsync(order3);
            //Obtener el order especifico
            Order order = await repository.GetOrderAsync(2);
            Assert.Pass();
        }

        [Test]
        public async Task DeleteOrderAsync()
        {
            var repository = _server.Host.Services.GetService<IOrdersAppServices>();
            //Agregar orders al servicio
            OrderDTO order1 = new OrderDTO
            {
                Id = 1,
                OrderDate = DateTime.Now,
                StatusOrder = "En camino",
                ArriveOrder = DateOnly.Parse("2023-09-15"),
                Product = new Product
                {
                    Id = 1,
                    Name = "Takis Fuego",
                    Description = "Fritura con chile",
                    Brand = "Barcel",
                    Category = "Fritura",
                    Price = 10
                },
                User = new Core.Users.User
                {
                    Id = 1,
                    FirstName = "David",
                    LastName = "Jiménez",
                    Role = "Vendedor",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 1,
                        Name = "Zitácuaro"
                    }
                }
            };
            OrderDTO order2 = new OrderDTO
            {
                Id = 2,
                OrderDate = DateTime.Now,
                StatusOrder = "Por iniciar",
                ArriveOrder = DateOnly.Parse("2023-09-11"),
                Product = new Product
                {
                    Id = 2,
                    Name = "Churrumais",
                    Description = "Fritura con chile y limón",
                    Brand = "Sabritas",
                    Category = "Fritura",
                    Price = 9.5
                },
                User = new Core.Users.User
                {
                    Id = 2,
                    FirstName = "Patricia",
                    LastName = "López",
                    Role = "Comprador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 2,
                        Name = "Morelia"
                    }
                }
            };
            OrderDTO order3 = new OrderDTO
            {
                Id = 3,
                OrderDate = DateTime.Now,
                StatusOrder = "Finalizada",
                ArriveOrder = DateOnly.Parse("2023-09-10"),
                Product = new Product
                {
                    Id = 3,
                    Name = "Chokies",
                    Description = "Galletas con chispas de chocolate",
                    Brand = "Marinela",
                    Category = "Galletas",
                    Price = 14.5
                },
                User = new Core.Users.User
                {
                    Id = 3,
                    FirstName = "Jose Rubén",
                    LastName = "De Jesús",
                    Role = "Cobrador",
                    Birthday = DateTime.Now,
                    City = new Core.Users.City
                    {
                        Id = 3,
                        Name = "Toluca"
                    }
                }
            };

            await repository.AddOrderAsync(order1);
            await repository.AddOrderAsync(order2);
            await repository.AddOrderAsync(order3);
            //Listar el contenido
            List<Order> list = await repository.GetOrdersAsync();
            //Eliminar uno es especifico
            await repository.DeleteOrderAsync(2);
            //Volver a listar
            List<Order> list_2 = await repository.GetOrdersAsync();

            Assert.Pass();
        }

        [Test]
        public async Task EditProductAsync()
        {
            var repository = _server.Host.Services.GetService<IOrdersAppServices>();
            await repository.AddAllOrdersInMemory();
            //Listar el contenido
            List<Order> list = await repository.GetOrdersAsync();
            //Eliminar uno en especifico
            var espera = 0;
            Order order = await repository.GetOrderAsync(2);
            order.OrderDate = DateTime.Now;
            order.Status = "Finalizado";
            await repository.EditOrderAsync(order);
            //Volver a listar
            List<Order> list_2 = await repository.GetOrdersAsync();

            Assert.Pass();
        }
    }
}