using Application.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Dtos;

namespace UnitTests.Application
{
    public class CreateSaleHandlerTests
    {
        [Fact]
        public async Task CreateSale_ShouldFail_WhenStockInsufficient()
        {
            var mockSales = new Mock<IRepository<Sale>>();
            var mockProducts = new Mock<IRepository<Product>>();
            var mockUnit = new Mock<IUnitOfWork>();

            var product = new Product("Keyboard", 100m, 1, null);

            mockProducts.Setup(r => r.GetByIdAsync(1))
                        .ReturnsAsync(product);

            var controller = new WebApi.Controllers.SalesController(
                mockSales.Object, mockProducts.Object, mockUnit.Object);

            var dto = new SaleCreateDto
            {
                Items = new()
            {
                new SaleItemDto
                {
                    ProductId = 1,
                    Quantity = 5 
                }
            }
            };

            var result = await controller.Create(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateSale_ShouldCreateSale()
        {
            var mockSales = new Mock<IRepository<Sale>>();
            var mockProducts = new Mock<IRepository<Product>>();
            var mockUnit = new Mock<IUnitOfWork>();

            var product = new Product("Keyboard", 100m, 10, null);

            mockProducts.Setup(r => r.GetByIdAsync(1))
                        .ReturnsAsync(product);

            mockUnit.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var controller = new WebApi.Controllers.SalesController(
                mockSales.Object, mockProducts.Object, mockUnit.Object);

            var dto = new SaleCreateDto
            {
                Items = new()
            {
                new SaleItemDto { ProductId = 1, Quantity = 2 }
            }
            };

            var result = await controller.Create(dto);

            mockSales.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
            mockUnit.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
