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
    public class ProductsController : ControllerBase
    {

        private readonly IRepository<Product> _repo;
        private readonly IUnitOfWork _unit;

        public ProductsController(IRepository<Product> repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        // GET paginado
        [HttpGet]
        public async Task<ActionResult<PagedResult<Product>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _repo.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        // GET por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST crear
        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateDto dto)
        {
            var product = new Product(dto.Name, dto.Price, dto.Stock, dto.ImageUrl);

            await _repo.AddAsync(product);
            await _unit.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT actualizar
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ProductCreateDto dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();

            product.Update(dto.Name, dto.Price, dto.Stock, dto.ImageUrl);

            _repo.Update(product);
            await _unit.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();

            _repo.Remove(product);
            await _unit.SaveChangesAsync();

            return NoContent();
        }
    }
}
