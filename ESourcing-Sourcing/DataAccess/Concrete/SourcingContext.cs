using ESourcing_Sourcing.DataAccess.Abstract;
using ESourcing_Sourcing.Entities;
using ESourcing_Sourcing.Settings.DatabaseSettings;
using MongoDB.Driver;

namespace ESourcing_Sourcing.DataAccess.Concrete
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auctions =database.GetCollection<Auction>(nameof(Auction));
            Bids =database.GetCollection<Bid>(nameof(Bid));


            SourcingContextSeed.SeedData(Auctions);
        }

        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
