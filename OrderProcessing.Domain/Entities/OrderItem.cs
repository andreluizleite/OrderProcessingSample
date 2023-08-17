using OrderProcessing.Domain.ValueObjects;

namespace OrderProcessing.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private OrderItem() { } // Private constructor for Entity Framework

        public OrderItem(Product product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        // Additional methods and properties as needed
    }
}