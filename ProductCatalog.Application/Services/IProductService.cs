using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.ProductApi.Application.DTOs;

namespace ProductCatalog.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int page, int pageSize);
        Task<bool> UpdateProductDescriptionAsync(int id, string description);
    }
}
