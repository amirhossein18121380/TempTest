using Domain.Entities.Product;

namespace Application.Contracts.Persistence;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetAllProductsWithRelatedUserAsync();
    Task<Product> GetProductsByIdAsync(int productId, bool trackEntity);
    Task DeleteProductAsync(int userId);
    Task<Product> GetProductsByEmailAsync(string email, bool trackEntity);
    Task<Product> GetProductByEmailAndDateAsync(string email, DateTime date, bool trackEntity);
    Task HardDeleteProductAsync(int productId);
}