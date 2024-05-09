using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices.Orders
{
    public interface IOrdersAppServices
    {
        Task<List<Order>> GetOrdersAsync();

        Task<int> AddOrderAsync(OrderDTO orderdto);

        Task DeleteOrderAsync(int orderId);
        Task<Order> GetOrderAsync(int orderId);
        Task<bool> ExistOrderAsync(int orderId);

        Task EditOrderAsync(Order order);

        Task AddAllOrdersInMemory();
    }
}
