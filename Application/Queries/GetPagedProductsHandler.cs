using Application.Abstractions;
using Application.Common;
using Domain.Entities;

namespace Application.Queries
{
    public class GetPagedProductsHandler
    {
        private readonly IRepository<Product> _repo;

        public GetPagedProductsHandler(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<Product>> HandleAsync(GetPagedProductsQuery request)
        {
            return await _repo.GetPagedAsync(
                request.Page,
                request.PageSize
            );
        }
    }
}
