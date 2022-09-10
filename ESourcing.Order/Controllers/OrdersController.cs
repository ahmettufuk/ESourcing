using ESourcing.Application.Commands.OrderCreate;
using ESourcing.Application.Queries;
using ESourcing.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Domain.Entities;

namespace ESourcing.Order.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetOrdersByUserName/{userName}")]
        public async Task<ActionResult<IEnumerable<OrderResponses>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersBySellerUsernameQuery(userName);
            var orders = await _mediator.Send(query);
            
            if (orders.Count() == decimal.Zero)
            {
                return NotFound();
            }
            

            return Ok(orders);
        }

        [HttpPost]

        public async Task<ActionResult<OrderResponses>> OrderCreate([FromBody] OrderCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        } 
    }
}
