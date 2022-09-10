using ESourcing_Sourcing.DataAccess.Abstract;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Repositories.Abstract;
using MongoDB.Driver;

namespace ESourcing_Sourcing.Repositories.Concrete
{
    public class BidRepository :IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }

        public async Task<List<Bid>> GetBidByAuctionId(string id)
        {
            var filter = Builders<Bid>.Filter.Eq(a => a.AuctionId, id);
            var bids=await _context.Bids.Find(filter).ToListAsync();

            return  bids.OrderByDescending(p => p.CreatedAt).GroupBy(p => p.SellerUserName).Select(p => new Bid
            {
                AuctionId = p.FirstOrDefault().AuctionId,
                Price = p.FirstOrDefault().Price,
                CreatedAt = p.FirstOrDefault().CreatedAt,
                SellerUserName = p.FirstOrDefault().SellerUserName,
                ProductId = p.FirstOrDefault().ProductId,
                Id = p.FirstOrDefault().Id

            }).ToList();
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> bids = await GetBidByAuctionId(id);
            return bids.OrderByDescending(p => p.Price).FirstOrDefault();
        }
    }
}
