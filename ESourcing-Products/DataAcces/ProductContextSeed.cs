using ESourcing_Products.Entitites;
using MongoDB.Driver;

namespace ESourcing_Products.DataAcces
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProduct());
            }
        }

        private static IEnumerable<Product> GetConfigureProduct()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name = "Samsung 10",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-1.png",
                    Price = 1500,
                    Category = "Phone"
                },
                new Product
                {
                    Name = "Samsung 11",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-2.png",
                    Price = 1600,
                    Category = "Phone"
                },
                new Product
                {
                    Name = "Samsung 12",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-3.png",
                    Price = 1700,
                    Category = "Phone"
                },
                new Product
                {
                    Name = "Samsung 13",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-4.png",
                    Price = 1800,
                    Category = "Phone"
                },
                new Product
                {
                    Name = "Samsung 14",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-5.png",
                    Price = 1900,
                    Category = "Phone"
                },
                new Product
                {
                    Name = "Samsung 15",
                    Summary = "Phone",
                    Description = "Great",
                    ImageFile = "product-6.png",
                    Price = 2000,
                    Category = "Phone"
                },
            };
        }
    }
}
