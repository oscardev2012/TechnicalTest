using Application.Abstractions;
using Application.Common;
using Application.Queries;
using Domain.Entities;
using Moq;

namespace UnitTests.Application
{
    public class GetPagedProductsHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnPagedResult()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Product>>();

            mockRepo.Setup(r => r.GetPagedAsync(1, 10))
                .ReturnsAsync(new PagedResult<Product>
                {
                    Items = new List<Product>
                    {
                        new Product("Laptop", 1500, 5, null)
                    },
                    TotalCount = 1,
                    Page = 1,
                    PageSize = 10
                });

            var handler = new GetPagedProductsHandler(mockRepo.Object);

            // Act
            var result = await handler.HandleAsync(new GetPagedProductsQuery(1, 10));

            // Assert
            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalCount);
        }
    }
}
