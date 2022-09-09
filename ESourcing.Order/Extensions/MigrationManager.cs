using ESourcing.Infrastracture.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace ESourcing.Order.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrationDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var orderContext = scope.ServiceProvider.GetService<OrderContext>();

                    if (orderContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        orderContext.Database.Migrate();
                    }

                    OrderContextSeed.SeedAsync(orderContext).Wait();
                }
                catch (Exception e)
                {
                    
                    throw;
                }
            }

            return host;
        }
    }
}
