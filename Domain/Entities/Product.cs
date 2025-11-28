using Domain.Commons;

namespace Domain.Entities
{
    // Domain/Entities/Product.cs
    public class Product : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string? ImageUrl { get; private set; }

        public Product(string name, decimal price, int stock, string? imageUrl = null)
        {
            Update(name, price, stock, imageUrl);
        }

        public void Update(string name, decimal price, int stock, string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
            if (price < 0) throw new ArgumentException("Price cannot be negative");
            if (stock < 0) throw new ArgumentException("Stock cannot be negative");

            Name = name.Trim();
            Price = price;
            Stock = stock;
            ImageUrl = imageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            if (Stock < quantity) throw new InvalidOperationException("Insufficient stock");
            Stock -= quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
