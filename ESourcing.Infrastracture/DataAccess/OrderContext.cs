using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;

namespace ESourcing.Infrastracture.DataAccess
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) :base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
    }
}
