using ESourcing_Products.Entitites;
using MongoDB.Driver;

namespace ESourcing_Products.DataAcces.Abstract
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; set; }
    }
}
