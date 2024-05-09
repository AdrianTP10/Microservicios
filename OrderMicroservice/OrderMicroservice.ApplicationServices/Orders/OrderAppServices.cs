using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.ApplicationServices.Orders;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using OrderMicroservice.DataAccess.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Orders
{
    public class OrderAppServices : IOrdersAppServices
    {
        private readonly IRepository<int, Order> _repository;
        private readonly IMapper _mapper;

        public OrderAppServices(IRepository<int, Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddOrderAsync(OrderDTO orderdto)
        {
            Order order = _mapper.Map<Order>(orderdto);
            await _repository.AddAsync(order);

            // Registro del evento
            Log.Information("The method was executed: AddOrderAsync");

            return order.Id;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            // Registro del evento
            Log.Information("The method was executed: DeleteOrderAsync");

            await _repository.DeleteAsync(orderId);
        }

        public async Task EditOrderAsync(Order order)
        {
            // Registro del evento
            Log.Information("The method was executed: EditOrderAsync");

            await _repository.UpdateAsync(order);
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            // Registro del evento
            Log.Information("The method was executed: GetOrderAsync");

            return await _repository.GetAll().Include(o => o.Product).Include(o => o.User).ThenInclude(u => u.City).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            // Registro del evento
            Log.Information("The method was executed: GetOrdersAsync");

            return await _repository.GetAll().Include(o => o.Product).Include(o => o.User).ThenInclude(u => u.City).ToListAsync();
        }

        public async Task<bool> ExistOrderAsync(int orderId)
        {
            // Registro del evento
            Log.Information("The method was executed: ExistOrderAsync");

            Order order = await _repository.GetAsync(orderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task AddAllOrdersInMemory()
        {
            Order order1 = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Status = "En camino",
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
            Order order2 = new Order
            {
                Id = 2,
                OrderDate = DateTime.Now,
                Status = "Por iniciar",
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
            Order order3 = new Order
            {
                Id = 3,
                OrderDate = DateTime.Now,
                Status = "Finalizada",
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
            await _repository.AddAsync(order1);
            await _repository.AddAsync(order2);
            await _repository.AddAsync(order3);

        }
    }
}
