using AutoMapper;
using ProductMicroservice.Core.Products;
using ProductMicroservice.Core.ProductsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMicroservice.AplicationServices
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile() 
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.DescriptionProduct, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryProduct, opt => opt.MapFrom(src => src.Category));

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.DescriptionProduct))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryProduct));
        }
    }
}
