using Domain.Entities;

namespace UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_ShouldSetProperties()
        {
            var product = new Product("Laptop", 1500m, 10, null);

            Assert.Equal("Laptop", product.Name);
            Assert.Equal(1500m, product.Price);
            Assert.Equal(10, product.Stock);
        }

        [Fact]
        public void UpdateProduct_ShouldModifyValues()
        {
            var product = new Product("Laptop", 1500m, 10, null);

            product.Update("Tablet", 800m, 5, "img.png");

            Assert.Equal("Tablet", product.Name);
            Assert.Equal(800m, product.Price);
            Assert.Equal(5, product.Stock);
            Assert.Equal("img.png", product.ImageUrl);
        }

        [Fact]
        public void DecreaseStock_ShouldThrow_WhenInsufficientStock()
        {
            var product = new Product("Mouse", 100m, 2, null);

            Assert.Throws<InvalidOperationException>(() => product.DecreaseStock(5));
        }

        [Fact]
        public void DecreaseStock_ShouldUpdateStock()
        {
            var product = new Product("Mouse", 100m, 10, null);

            product.DecreaseStock(3);

            Assert.Equal(7, product.Stock);
        }
    }
}
