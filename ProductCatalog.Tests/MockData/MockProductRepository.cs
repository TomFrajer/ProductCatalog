namespace ProductCatalog.Tests.MockData
{
    using ProductCatalog.Domain.Entities;
    using ProductCatalog.Domain.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ProductApi.Infrastructure.Repositories
    {
        public class MockProductRepository : IProductRepository
        {
            private readonly List<Product> _mockProducts;

            public MockProductRepository()
            {
                // Initialize mock data
                _mockProducts = new List<Product>
                {
                    new Product { Id = 1, Name = "Mock Product 1", ImgUri = "/images/mock1.png", Price = 100, Description = "This is a mock product." },
                    new Product { Id = 2, Name = "Mock Product 2", ImgUri = "/images/mock2.png", Price = 2000, Description = "Another mock product." },
                    new Product { Id = 3, Name = "Mock Product 3", ImgUri = "/images/mock3.png", Price = 1550 }
                };
            }

            public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
            {
                // Simulate respecting cancellation
                cancellationToken.ThrowIfCancellationRequested();
                return Task.FromResult(_mockProducts.AsEnumerable());
            }

            public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var product = _mockProducts.FirstOrDefault(p => p.Id == id);
                return Task.FromResult(product);
            }

            public Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var pagedProducts = _mockProducts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .AsEnumerable();
                return Task.FromResult(pagedProducts);
            }

            public Task<bool> UpdateDescriptionAsync(int id, string description, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var product = _mockProducts.FirstOrDefault(p => p.Id == id);
                if (product == null) return Task.FromResult(false);

                product.Description = description;
                return Task.FromResult(true);
            }
        }
    }
}
