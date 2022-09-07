using ESourcing_Sourcing.Entities;

namespace ESourcing_Sourcing.Repositories.Abstract
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
