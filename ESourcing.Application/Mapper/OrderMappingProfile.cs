using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ESourcing.Application.Commands.OrderCreate;
using ESourcing.Application.Responses;
using Ordering.Domain.Entities;

namespace ESourcing.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderCreateCommand>().ReverseMap();
            CreateMap<Order,OrderResponses>().ReverseMap();
        }
    }
}
