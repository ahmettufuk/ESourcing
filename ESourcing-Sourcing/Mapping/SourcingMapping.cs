using AutoMapper;
using ESourcing_Sourcing.Entities;
using EventBusRabbitMQ.Events;

namespace ESourcing_Sourcing.Mapping
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
