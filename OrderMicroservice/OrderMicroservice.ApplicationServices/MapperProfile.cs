using OrderMicroservice.Core.Orders;
using OrderMicroservice.Core.OrdersDTO;
using OrderMicroservice.Core.Products;
using OrderMicroservice.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.ApplicationServices
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile() 
        {
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.StatusOrder, opt => opt.MapFrom(src => src.Status));
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusOrder));
            CreateMap<OrderClean, OrderDTO>().ForMember(dest => dest.StatusOrder, opt => opt.MapFrom(src => src.Status));
            CreateMap<OrderClean, Order>().ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product { Id = src.IdProduct }))
                                          .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { Id = src.IdUser }));
            CreateMap<Order, OrderClean>().ForMember(dest => dest.IdProduct, opt => opt.MapFrom(src => src.Product.Id))
                                          .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.User.Id));
            CreateMap<OrderDTO, OrderClean>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusOrder));
            CreateMap<OrderWithProduct, OrderDTO>().ForMember(dest => dest.StatusOrder, opt => opt.MapFrom(src => src.Status));
            CreateMap<OrderWithUserWithCity, OrderDTO>().ForMember(dest => dest.StatusOrder, opt => opt.MapFrom(src => src.Status));
            CreateMap<OrderWithUser, OrderDTO>().ForMember(dest => dest.StatusOrder, opt => opt.MapFrom(src => src.Status));
            CreateMap<UserDTO, User>();
            CreateMap<Order, OrderWithUserWithCity>();

        }
    }
}
