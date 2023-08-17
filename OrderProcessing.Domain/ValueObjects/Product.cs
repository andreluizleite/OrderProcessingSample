namespace OrderProcessing.Domain.ValueObjects
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        // Private constructor for Entity Framework
        private Product() 
        {
            Id = Guid.NewGuid(); 
        }

        public Product(Guid id)
        {
            Id = id;
        }

        public Product(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
