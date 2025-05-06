using ElasticSearchDemo.API.Models;

namespace ElasticSearchDemo.API.Services;

public interface IProductService
{
    Task CreateProductAsync(Product product);
    Task<Product> GetProductByIdAsync(string id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(string id);
    Task BulkCreateProductsAsync(IEnumerable<Product> products);
    Task BulkUpdateProductsAsync(IEnumerable<Product> products);
    Task BulkDeleteProductsAsync(IEnumerable<string> ids);
}