using ESourcing_Sourcing.DataAccess.Abstract;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Repositories.Abstract;
using MongoDB.Driver;

namespace ESourcing_Sourcing.Repositories.Concrete
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ISourcingContext _context;

        public AuctionRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _context.Auctions.Find(p=>true).ToListAsync();
        }

        public async Task<Auction> GetAuctionById(string id)
        {
            //FilterDefinition<Auction> filterDefinition = Builders<Auction>.Filter.Eq(p => p.Id, id);
            return await _context.Auctions.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            return await _context.Auctions.Find(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task Create(Auction auction)
        {
            await _context.Auctions.InsertOneAsync(auction);
        }

        public async Task<bool> Update(Auction auction)
        {
            var updatedAuction = await _context.Auctions.ReplaceOneAsync(p=>p.Id.Equals(auction.Id),auction);
            return updatedAuction.IsAcknowledged && updatedAuction.ModifiedCount>0;

        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Auctions.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
