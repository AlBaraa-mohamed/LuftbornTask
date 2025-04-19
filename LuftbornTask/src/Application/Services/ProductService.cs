using LuftbornTask.src.Domain.Entities;
using LuftbornTask.src.Domain.Interfaces;
using LuftbornTask.src.WebUI.Strategies;

namespace LuftbornTask.src.Application.Services
{
    public delegate IPriceStrategy StrategyFactory(string strategyName);

    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly StrategyFactory _strategyFactory;

        public ProductService(IProductRepository repository, StrategyFactory strategyFactory)
        {
            _repository = repository;
            _strategyFactory = strategyFactory;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Product product, string strategyName)
        {
            var strategy = _strategyFactory(strategyName);
            product.Price = strategy.CalculatePrice(product.Price);
            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product, string strategyName)
        {
            var strategy = _strategyFactory(strategyName);
            product.Price = strategy.CalculatePrice(product.Price);
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllByUserAsync(string? userId)
        {
            return await _repository.GetAllByUserAsync(userId);
        }
    }
}
