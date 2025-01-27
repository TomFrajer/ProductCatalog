using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<bool> UpdateDescriptionAsync(int id, string description, CancellationToken cancellationToken);
    }
}

