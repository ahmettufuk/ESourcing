using ESourcing_Products.DataAcces.Abstract;
using ESourcing_Products.Entitites;
using ESourcing_Products.Settings.ConfigurationSettings;
using MongoDB.Driver;

namespace ESourcing_Products.DataAcces.Concrete
{
    public class ProductContext :IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);

            ProductContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; set; }
    }
}
