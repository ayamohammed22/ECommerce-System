﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("OrderConnectionString"),
                     sqlOption =>
                     {
                         sqlOption.EnableRetryOnFailure();
                     });
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
