using AutoMapper;
using PaymentMicroservice.Core.Payments;
using PaymentMicroservice.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.ApplicationServices
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<PaymentDto, Payment>();

            CreateMap<Payment, PaymentDto>();
        }
    }
}
