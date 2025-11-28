using Application.Common;

namespace Application.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<PagedResult<T>> GetPagedAsync(int page, int pageSize);

        Task<List<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
