using System.Net;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing_Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _bidsRepository;

        public BidsController(IBidRepository bidsRepository)
        {
            _bidsRepository = bidsRepository;
        }

        [HttpPost]
        
        public async Task<ActionResult> sendBid([FromBody] Bid bid)
        {
            _bidsRepository.SendBid(bid);
            return Ok();
        }

        [HttpGet("GetBidByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidByAuctionId(string id)
        {
            var bids = await _bidsRepository.GetBidByAuctionId(id);
            return Ok(bids);
        }

        [HttpGet("GetWinnerBid")]
        [ProducesResponseType(typeof(Bid),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bid>> GetWinnerBid(string id)
        {
            var winner = await _bidsRepository.GetWinnerBid(id);
            return Ok(winner);
        }
    }
}
