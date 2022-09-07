using ESourcing_Sourcing.Entities;
using MongoDB.Driver;

namespace ESourcing_Sourcing.DataAccess.Abstract
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
