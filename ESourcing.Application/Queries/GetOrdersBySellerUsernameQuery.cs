using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESourcing.Application.Responses;
using MediatR;
using Ordering.Domain.Entities;

namespace ESourcing.Application.Queries
{
    public class GetOrdersBySellerUsernameQuery : IRequest<IEnumerable<OrderResponses>>
    {
        public string UserName { get; set; }

        public GetOrdersBySellerUsernameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
