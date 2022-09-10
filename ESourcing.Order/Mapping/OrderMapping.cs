using AutoMapper;
using ESourcing.Application.Commands.OrderCreate;
using EventBusRabbitMQ.Events;

namespace ESourcing.Order.Mapping
{
    public class OrderMapping :Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
