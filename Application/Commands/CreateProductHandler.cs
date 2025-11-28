using Application.Abstractions;
using Application.Features;
using Domain.Entities;

namespace Application.Commands
{
    public class CreateProductHandler
    {
        private readonly IRepository<Product> _repo;
        private readonly IUnitOfWork _unit;

        public CreateProductHandler(IRepository<Product> repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<int> HandleAsync(CreateProductCommand request)
        {
            var product = new Product(request.Name, request.Price, request.Stock, request.ImageUrl);

            await _repo.AddAsync(product);
            await _unit.SaveChangesAsync();

            return product.Id;
        }
    }
}
