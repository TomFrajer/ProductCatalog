using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.ProductApi.Application.DTOs;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : new ProductDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int page, int pageSize)
        {
            var products = await _repository.GetPagedAsync(page, pageSize);
            return products.Select(p => new ProductDto(p));
        }

        public async Task<bool> UpdateProductDescriptionAsync(int id, string description)
        {
            return await _repository.UpdateDescriptionAsync(id, description);
        }
    }

}
