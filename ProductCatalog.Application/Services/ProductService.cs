using ProductCatalog.Application.DTOs;
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

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return products.Select(p => new ProductDto(p));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            return product == null ? null : new ProductDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var products = await _repository.GetPagedAsync(page, pageSize, cancellationToken);
            return products.Select(p => new ProductDto(p));
        }

        public async Task<bool> UpdateProductDescriptionAsync(int id, string description, CancellationToken cancellationToken)
        {
            return await _repository.UpdateDescriptionAsync(id, description, cancellationToken);
        }
    }
}
