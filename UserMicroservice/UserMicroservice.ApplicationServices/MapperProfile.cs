using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMicroservice.Core.Users;
using UserMicroservice.Users.Dto;

namespace UserMicroservice.ApplicationServices
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>();

            //CreateMap<Core.Accounts.User, UserDto>()
            //    .ForMember(dest => dest.Name,
            //        opt => opt.MapFrom(src => src.UserName));



        }

    }
}
