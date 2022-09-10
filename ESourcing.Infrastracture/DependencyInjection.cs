﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESourcing.Infrastracture.DataAccess;
using ESourcing.Infrastracture.Repositories.Base;
using ESourcing.Infrastracture.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Ordering.Domain.Repositories.Abstract;
using Ordering.Domain.Repositories.Base;

namespace ESourcing.Infrastracture
{
    public static class DependencyInjection
    {
      

        
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            /*
            services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
                                                ServiceLifetime.Singleton,
                                              ServiceLifetime.Singleton);
        */

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("OrderConnection"),
                    b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);

            //Add Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
