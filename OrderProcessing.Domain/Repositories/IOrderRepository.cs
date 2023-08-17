using System;
using System.Threading.Tasks;
using OrderProcessing.Domain.Entities;

namespace OrderProcessing.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
