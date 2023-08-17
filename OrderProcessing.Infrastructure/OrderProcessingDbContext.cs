using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderProcessing.Infrastructure
{
    public class OrderProcessingDbContext : DbContext
    {
        public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        // Add other DbSet properties for other entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
        }
    }
}
