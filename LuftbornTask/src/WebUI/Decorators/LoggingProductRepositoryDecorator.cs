using LuftbornTask.src.Domain.Entities;
using LuftbornTask.src.Domain.Interfaces;

namespace LuftbornTask.src.WebUI.Decorators
{
    public class LoggingProductRepositoryDecorator : IProductRepository
    {
        private readonly IProductRepository _inner;
        private readonly ILogger<LoggingProductRepositoryDecorator> _logger;

        public LoggingProductRepositoryDecorator(IProductRepository inner, ILogger<LoggingProductRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Getting all products");
            return await _inner.GetAllAsync();
        }
        public async Task<IEnumerable<Product>> GetAllByUserAsync(string? UserId)
        {
            _logger.LogInformation("Getting all products by user Id");
            return await _inner.GetAllByUserAsync(UserId);
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting product by ID: {Id}", id);
            return await _inner.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            _logger.LogInformation("Adding a new product: {@Product}", product);
            await _inner.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _logger.LogInformation("Updating product: {@Product}", product);
            await _inner.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            await _inner.DeleteAsync(id);
        }
    }
}
