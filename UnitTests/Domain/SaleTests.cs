using Domain.Entities;

namespace UnitTests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void CreateSale_ShouldSetItems()
        {
            var items = new List<SaleItem>
            {
                new SaleItem(1, 100m, 2),
                new SaleItem(2, 50m, 1)
            };

            var sale = new Sale(items);

            Assert.Equal(2, sale.Items.Count);
            Assert.Equal(250m, sale.Total);
        }

        [Fact]
        public void CreateSale_ShouldThrow_WhenNoItems()
        {
            Assert.Throws<ArgumentException>(() => new Sale(new List<SaleItem>()));
        }

        [Fact]
        public void SaleItem_Total_ShouldBeCorrect()
        {
            var item = new SaleItem(1, 100m, 3);

            Assert.Equal(300m, item.Total);
        }
    }
}
