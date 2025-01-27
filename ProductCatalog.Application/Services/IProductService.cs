using ProductCatalog.Application.DTOs;

namespace ProductCatalog.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
        Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<bool> UpdateProductDescriptionAsync(int id, string description, CancellationToken cancellationToken);
    }
}

