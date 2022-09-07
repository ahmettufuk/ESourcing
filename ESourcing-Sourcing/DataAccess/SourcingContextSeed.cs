using ESourcing_Sourcing.Entities;
using MongoDB.Driver;

namespace ESourcing_Sourcing.DataAccess
{
    public  class SourcingContextSeed
    {
        public static void SeedData(IMongoCollection<Auction> auctionCollection)
        {
            bool exist = auctionCollection.Find(p=>true).Any();
            if (!exist)
            {
                auctionCollection.InsertManyAsync(GetPreConfiguredAuctions());
            }
        }


        private static IEnumerable<Auction> GetPreConfiguredAuctions()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    Name = "Auction 1",
                    Description = "Lorem Ipsum",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "6313784faa513ffe099a07b1",
                    Quantity = 5,
                    Status = (int)Status.Active,
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    }
                },
                new Auction()
                {
                    Name = "Auction 2",
                    Description = "Lorem Ipsum2",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "6313784faa513ffe099a07b2",
                    Quantity = 5,
                    Status = (int)Status.Active,
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    }
                },
                new Auction()
                {
                    Name = "Auction 3",
                    Description = "Lorem Ipsum3",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "6313784faa513ffe099a07b3",
                    Quantity = 5,
                    Status = (int)Status.Active,
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    }
                },
                new Auction()
                {
                    Name = "Auction 4",
                    Description = "Lorem Ipsum4",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "6313784faa513ffe099a07b4",
                    Quantity = 5,
                    Status = (int)Status.Active,
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    }
                },
                new Auction()
                {
                    Name = "Auction 5",
                    Description = "Lorem Ipsum5",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    ProductId = "6313784faa513ffe099a07b5",
                    Quantity = 5,
                    Status = (int)Status.Active,
                    IncludedSellers = new List<string>()
                    {
                        "seller1@test.com",
                        "seller2@test.com",
                        "seller3@test.com"
                    }
                }
            };



        }
    }
}
