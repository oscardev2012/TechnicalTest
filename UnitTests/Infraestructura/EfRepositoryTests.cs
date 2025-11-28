using Domain.Entities;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infraestructura
{
    public class EfRepositoryTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // BD única por test
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            var context = CreateContext();
            var repo = new EfRepository<Product>(context);

            var product = new Product("Laptop", 1500, 10, null);

            await repo.AddAsync(product);
            await context.SaveChangesAsync();

            Assert.Equal(1, context.Products.Count());
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedItems()
        {
            var context = CreateContext();
            var repo = new EfRepository<Product>(context);

            for (int i = 1; i <= 20; i++)
            {
                await repo.AddAsync(new Product($"Product {i}", i * 10, i, null));
            }

            await context.SaveChangesAsync();

            var result = await repo.GetPagedAsync(1, 10);

            Assert.Equal(10, result.Items.Count());
            Assert.Equal(20, result.TotalCount);
        }
    }
}
