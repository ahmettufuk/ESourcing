using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ESourcing.Application.Commands.OrderCreate;
using ESourcing.Application.Responses;
using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories.Abstract;

namespace ESourcing.Application.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand,OrderResponses>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponses> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            if (orderEntity ==null)
            {
                throw new ApplicationException("Entity could not be mapped!");
            }

            var order = await _orderRepository.AddAsync(orderEntity);
            var orderResponse = _mapper.Map<OrderResponses>(order);

            return orderResponse;
        }
    }
}
