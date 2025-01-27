using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateDescriptionAsync(int id, string description, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product == null) return false;

            product.Description = description;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
