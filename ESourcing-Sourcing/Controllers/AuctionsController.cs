using System.Net;
using AutoMapper;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Repositories.Abstract;
using ESourcing_Sourcing.Repositories.Concrete;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing_Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionsController> _logger;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _rabbitmqProducer;

        public AuctionsController(IAuctionRepository auctionRepository,ILogger<AuctionsController> logger, IBidRepository bidRepository, IMapper mapper, EventBusRabbitMQProducer rabbitmqProducer)
        {
            _logger = logger;
            _bidRepository = bidRepository;
            _mapper = mapper;
            _rabbitmqProducer = rabbitmqProducer;
            _auctionRepository = auctionRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult< IEnumerable<Auction>>> GetAuctions()
        {
            var auctions = await _auctionRepository.GetAuctions();
            return  Ok(auctions);
        }

        [HttpGet("{id:length(24)}",Name = "GetAuction")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var auction = await _auctionRepository.GetAuctionById(id);

            if (auction ==null)
            {
                _logger.LogError($"Auctions with id: {id} , has not been found in database");
                return BadRequest();
            } 
            return Ok(auction);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.Create(auction);
            return CreatedAtRoute("GetAuction",new{id =auction.Id});
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> UpdateAuction([FromBody] Auction auction)
        {
           var updatedAuction= await _auctionRepository.Update(auction);
           return Ok(updatedAuction);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> DeleteAuction(string id)
        {
            var deletedAuction = await _auctionRepository.Delete(id);
            return Ok(deletedAuction);
        }

        [HttpPost("CompleteAuction")]
        public async Task<ActionResult> CompleteAuction(string id)
        {
            var auction = await _auctionRepository.GetAuctionById(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (auction.Status !=(int)Status.Active)
            {
                _logger.LogError("Auction can not be completed");
                return BadRequest();
            }

            var bid = await _bidRepository.GetWinnerBid(id);
            if (bid == null)
            {
                return NotFound();
            }

            OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity=auction.Quantity;
            auction.Status = (int)Status.Closed;
            bool updateResponse = await _auctionRepository.Update(auction);
            if (!updateResponse)
            {
                _logger.LogError("Auction can not Updated");
                return BadRequest();
            }

            try
            {
                _rabbitmqProducer.Publish(EventBusConstants.OrderCreateQueue,eventMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"ERROR Publishing integration event: {EventId} from {AppName}",eventMessage.Id,"Sourcing");
            }

            return Accepted();
        }

        [HttpPost("TestEvent")]
        public ActionResult<OrderCreateEvent> TestEvent()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "dummy1";
            eventMessage.ProductId = "dummy_product_1";
            eventMessage.Price = 10;
            eventMessage.Quantity = 100;
            eventMessage.SellerUserName = "test@test.com";

            try
            {
                _rabbitmqProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted(eventMessage);
        }
    }
}
