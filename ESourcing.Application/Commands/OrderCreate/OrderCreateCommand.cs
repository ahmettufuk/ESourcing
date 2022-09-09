using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESourcing.Application.Responses;
using MediatR;

namespace ESourcing.Application.Commands.OrderCreate
{
    public class OrderCreateCommand : IRequest<OrderResponses>
    {
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
