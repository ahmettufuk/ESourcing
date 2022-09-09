using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ESourcing.Application.Queries;
using ESourcing.Application.Responses;
using MediatR;
using Ordering.Domain.Repositories.Abstract;

namespace ESourcing.Application.Handlers
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersBySellerUsernameQuery,IEnumerable<OrderResponses>>
    {
        private readonly  IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponses>> Handle(GetOrdersBySellerUsernameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersBySellerUserName(request.UserName);
            var response = _mapper.Map<IEnumerable<OrderResponses>>(orderList);

            return response;
        }
    }
}
