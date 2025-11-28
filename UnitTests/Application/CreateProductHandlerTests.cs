using Application.Abstractions;
using Application.Commands;
using Application.Features;
using Domain.Entities;
using Moq;

namespace UnitTests.Application
{
    public class CreateProductHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateProduct()
        {
            var mockRepo = new Mock<IRepository<Product>>();
            var mockUnit = new Mock<IUnitOfWork>();

            mockUnit.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var handler = new CreateProductHandler(mockRepo.Object, mockUnit.Object);

            var cmd = new CreateProductCommand("Laptop", 1000m, 10, null);

            var id = await handler.HandleAsync(cmd);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
            mockUnit.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
