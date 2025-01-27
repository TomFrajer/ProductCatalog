using Moq;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.UnitTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product", Price = 100 } };
            var cancellationToken = CancellationToken.None; // Simulated token
            _mockRepo.Setup(repo => repo.GetAllAsync(cancellationToken)).ReturnsAsync(products);

            // Act
            var result = await _service.GetProductsAsync(cancellationToken);

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Product", result.First().Name);
        }
    }
}
