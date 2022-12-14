using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories.Base;

namespace Ordering.Domain.Repositories.Abstract
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName);
    }
}
