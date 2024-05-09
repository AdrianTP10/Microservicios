using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderMicroservice.ApplicationServices.Cities;
using OrderMicroservice.ApplicationServices.Orders;
using OrderMicroservice.ApplicationServices.Products;
using OrderMicroservice.ApplicationServices.Users;
using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using Org.BouncyCastle.Asn1.X509;

namespace OrderMicroservice.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersAppServices _ordersAppServices;
        private readonly IProductAppServices _productsAppServices;
        private readonly IUserAppServices _usersAppServices;
        private readonly ICityAppServices _citiesAppServices;
        private readonly IMapper _mapper;

        public OrderController(IOrdersAppServices ordersAppServices, IMapper mapper, IProductAppServices productAppServices, IUserAppServices userAppServices, ICityAppServices cityAppServices)
        {
            _ordersAppServices = ordersAppServices;
            _mapper = mapper;
            _productsAppServices = productAppServices;
            _usersAppServices = userAppServices;
            _citiesAppServices = cityAppServices;
        }

        //Lista todos los order que hay
        [HttpGet]
        public async Task<List<Order>> GetAll()
        {
            List<Order> orders = await _ordersAppServices.GetOrdersAsync();
            return orders;
        }

        //Extrae un order en base a un id
        [HttpGet(nameof(GetById))]
        public async Task<string> GetById(int orderId)
        {
            bool orderExist = await _ordersAppServices.ExistOrderAsync(orderId);
            if (!orderExist)
            {
                return "The order id '" + orderId + "' doesn't exist";
            }
            Order order = await _ordersAppServices.GetOrderAsync(orderId);
            string stringOrder = JsonConvert.SerializeObject(order, Formatting.Indented);
            return stringOrder;
        }

        //Guarda un nuevo order pasando los demas atributos como son el product, user y city
        [HttpPost(nameof(AddOrderComplete))]
        public async Task<string> AddOrderComplete([FromBody] OrderDTO orderdto)
        {
            int id = await _ordersAppServices.AddOrderAsync(orderdto);
            return id.ToString();
        }

        //Guarda un nuevo order solo con los datos de order pasando solamente el id del user y del product
        [HttpPost(nameof(AddOrderClean))]
        public async Task<string> AddOrderClean([FromBody] OrderClean orderclean)
        {
            bool productExist = await _productsAppServices.ExistProductAsync(orderclean.IdProduct);
            if (!productExist)
            {
                return "The product id '" + orderclean.IdProduct + "' doesn't exist";
            }
            bool userExist = await _usersAppServices.ExistUserAsync(orderclean.IdUser);
            if (!userExist)
            {
                return "The user id '"+orderclean.IdUser+"' doesn't exist";
            }
            OrderDTO orderdto = _mapper.Map<OrderDTO>(orderclean);
            orderdto.Product = await _productsAppServices.GetProductAsync(orderclean.IdProduct);
            orderdto.User = await _usersAppServices.GetUserAsync(orderclean.IdUser);
            int id = await _ordersAppServices.AddOrderAsync(orderdto);
            return id.ToString();
        }

        //Guarda un nuevo order solo con los datos de order pasando solamente el id del user y creando un nuevo product
        [HttpPost(nameof(AddOrderWithProduct))]
        public async Task<string> AddOrderWithProduct([FromBody] OrderWithProduct orderwithproduct)
        {
            bool userExist = await _usersAppServices.ExistUserAsync(orderwithproduct.IdUser);
            if (!userExist)
            {
                return "The user id '"+ orderwithproduct.IdUser+"' doesn't exist";
            }
            OrderDTO orderdto = _mapper.Map<OrderDTO>(orderwithproduct);
            orderdto.User = await _usersAppServices.GetUserAsync(orderwithproduct.IdUser);
            int id = await _ordersAppServices.AddOrderAsync(orderdto);
            return id.ToString();
        }

        //Guarda un nuevo order solo con los datos de order pasando solamente el id del product y creando un nuevo user indicando el id de city
        [HttpPost(nameof(AddOrderWithUser))]
        public async Task<string> AddOrderWithUser([FromBody] OrderWithUser orderwithuser)
        {
            bool productExist = await _productsAppServices.ExistProductAsync(orderwithuser.IdProduct);
            if (!productExist)
            {
                return "The product id '" + orderwithuser.IdProduct + "' doesn't exist";
            }
            bool cityExist = await _citiesAppServices.ExistCityAsync(orderwithuser.UserDTO.IdCity);
            if (!cityExist)
            {
                return "The city id '" + orderwithuser.UserDTO.IdCity + "' doesn't exist";
            }
            OrderDTO orderdto = _mapper.Map<OrderDTO>(orderwithuser);
            orderdto.Product = await _productsAppServices.GetProductAsync(orderwithuser.IdProduct);
            orderdto.User = _mapper.Map<User>(orderwithuser.UserDTO);
            orderdto.User.City = await _citiesAppServices.GetCityAsync(orderwithuser.UserDTO.IdCity);
            int id = await _ordersAppServices.AddOrderAsync(orderdto);
            return id.ToString();
        }

        //Guarda un nuevo order solo con los datos de order pasando solamente el id del product y creando un nuevo user y con una nueva city
        [HttpPost(nameof(AddOrderWithUserWithCity))]
        public async Task<string> AddOrderWithUserWithCity([FromBody] OrderWithUserWithCity orderwithuser)
        {
            bool productExist = await _productsAppServices.ExistProductAsync(orderwithuser.IdProduct);
            if (!productExist)
            {
                return "The product id '" + orderwithuser.IdProduct + "' doesn't exist";
            }
            OrderDTO orderdto = _mapper.Map<OrderDTO>(orderwithuser);
            orderdto.Product = await _productsAppServices.GetProductAsync(orderwithuser.IdProduct);
            int id = await _ordersAppServices.AddOrderAsync(orderdto);
            return id.ToString();
        }

        //Editar un order cambiado los datos unicamente de la tabla order
        [HttpPut(nameof(EditOrder))]
        public async Task<string> EditOrder([FromBody] OrderClean orderclean)
        {
            bool orderExist = await _ordersAppServices.ExistOrderAsync(orderclean.Id);
            if (!orderExist)
            {
                return "The order id '" + orderclean.Id + "' doesn't exist";
            }
            bool productExist = await _productsAppServices.ExistProductAsync(orderclean.IdProduct);
            if (!productExist)
            {
                return "The product id '" + orderclean.IdProduct + "' doesn't exist";
            }
            bool userExist = await _usersAppServices.ExistUserAsync(orderclean.IdUser);
            if (!userExist)
            {
                return "The user id '" + orderclean.IdUser + "' doesn't exist";
            }
            Order order = await _ordersAppServices.GetOrderAsync(orderclean.Id);
            order.OrderDate = orderclean.OrderDate;
            order.Status = orderclean.Status;
            order.Product = await _productsAppServices.GetProductAsync(orderclean.IdProduct);
            order.User = await _usersAppServices.GetUserAsync(orderclean.IdUser);
            await _ordersAppServices.EditOrderAsync(order);
            return "Order with the id '"+order.Id+"' update with exit! ";
        }

        //Eliminar un registro de la tabla order
        [HttpDelete(nameof(DeleteOrder))]
        public async Task<string> DeleteOrder(int orderId)
        {
            await _ordersAppServices.DeleteOrderAsync(orderId);
            return "Order with the id '" + orderId + "' delete with exit! ";
        }


    }
}
