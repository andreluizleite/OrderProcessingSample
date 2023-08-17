using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.Repositories;

namespace OrderProcessing.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderProcessingDbContext _dbContext;

        public OrderRepository(OrderProcessingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            //I will not implement the DB at that moment
           // await _dbContext.Orders.AddAsync(order);
           // await _dbContext.SaveChangesAsync();
        }
    }
}
