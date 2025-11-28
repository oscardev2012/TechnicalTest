using Application.Abstractions;
using Application.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly IRepository<Sale> _salesRepo;
        private readonly IRepository<Product> _productsRepo;
        private readonly IUnitOfWork _unit;

        public SalesController(
            IRepository<Sale> salesRepo,
            IRepository<Product> productsRepo,
            IUnitOfWork unit)
        {
            _salesRepo = salesRepo;
            _productsRepo = productsRepo;
            _unit = unit;
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaleCreateDto dto)
        {
            var items = new List<SaleItem>();

            foreach (var item in dto.Items)
            {
                var product = await _productsRepo.GetByIdAsync(item.ProductId);
                if (product == null) return BadRequest($"Producto {item.ProductId} no existe");
                if (product.Stock < item.Quantity) return BadRequest($"Stock insuficiente de {product.Name}");

                product.DecreaseStock(item.Quantity);
                _productsRepo.Update(product);

                items.Add(new SaleItem(item.ProductId, product.Price, item.Quantity));
            }

            var sale = new Sale(items);

            await _salesRepo.AddAsync(sale);
            await _unit.SaveChangesAsync();

            return Ok(new { saleId = sale.Id });
        }

        [HttpGet("report")]
        public async Task<ActionResult<PagedResult<SaleDto>>> GetReport(
            DateTime from,
            DateTime to,
            int page = 1,
            int pageSize = 10)
        {
            var query = _salesRepo
                .GetAllAsync()
                .Result
                .AsQueryable()
                .Where(s => s.SaleDate >= from && s.SaleDate <= to);

            var total = query.Count();

            var result = query
                .OrderByDescending(s => s.SaleDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SaleDto
                {
                    Id = s.Id,
                    SaleDate = s.SaleDate,
                    Total = s.Total,
                    ItemsCount = s.Items.Count
                })
                .ToList();

            return Ok(new PagedResult<SaleDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Items = result
            });
        }
    }
}
