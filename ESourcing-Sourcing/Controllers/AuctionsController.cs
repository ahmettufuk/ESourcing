using System.Net;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Repositories.Abstract;
using ESourcing_Sourcing.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing_Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly ILogger<AuctionsController> _logger;

        public AuctionsController(IAuctionRepository auctionRepository,ILogger<AuctionsController> logger)
        {
            _logger = logger;
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
    }
}
