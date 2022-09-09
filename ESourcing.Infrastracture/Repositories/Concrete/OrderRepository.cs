using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using ESourcing.Infrastracture.DataAccess;
using ESourcing.Infrastracture.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories.Abstract;
using Ordering.Domain.Repositories.Base;

namespace ESourcing.Infrastracture.Repositories.Concrete
{
    public class OrderRepository : Repository<Order>,IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _dbContext.Orders.Where(o => o.SellerUserName == userName).ToListAsync();
            return orderList;
        }
    }
}
