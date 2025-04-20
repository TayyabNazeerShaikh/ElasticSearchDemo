using ElasticSearchDemo.API.Models;
using ElasticSearchDemo.API.Services;
using Nest;

namespace ElasticSearchDemo.Services;

public class ProductService : IProductService
{
    private readonly IElasticClient _elasticClient;

    public ProductService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task CreateProductAsync(Product product)
    {
        var response = await _elasticClient.IndexDocumentAsync(product);
        if (!response.IsValid)
            throw new Exception($"Failed to create product: {response.OriginalException.Message}");
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        var response = await _elasticClient.GetAsync<Product>(id);
        if (!response.IsValid)
            throw new Exception($"Failed to get product: {response.OriginalException.Message}");
        return response.Source;
    }

    public async Task UpdateProductAsync(Product product)
    {
        var response = await _elasticClient.UpdateAsync<Product>(product.Id, u => u.Doc(product));
        if (!response.IsValid)
            throw new Exception($"Failed to update product: {response.OriginalException.Message}");
    }

    public async Task DeleteProductAsync(string id)
    {
        var response = await _elasticClient.DeleteAsync<Product>(id);
        if (!response.IsValid)
            throw new Exception($"Failed to delete product: {response.OriginalException.Message}");
    }

    public async Task BulkCreateProductsAsync(IEnumerable<Product> products)
    {
        var bulkDescriptor = new BulkDescriptor();
        foreach (var product in products)
        {
            bulkDescriptor.Index<Product>(op => op.Document(product));
        }
        var response = await _elasticClient.BulkAsync(bulkDescriptor);
        if (!response.IsValid)
            throw new Exception($"Failed to bulk create products: {response.OriginalException.Message}");
    }

    public async Task BulkUpdateProductsAsync(IEnumerable<Product> products)
    {
        var bulkDescriptor = new BulkDescriptor();
        foreach (var product in products)
        {
            bulkDescriptor.Update<Product>(op => op.Id(product.Id).Doc(product));
        }
        var response = await _elasticClient.BulkAsync(bulkDescriptor);
        if (!response.IsValid)
            throw new Exception($"Failed to bulk update products: {response.OriginalException.Message}");
    }

    public async Task BulkDeleteProductsAsync(IEnumerable<string> ids)
    {
        var bulkDescriptor = new BulkDescriptor();
        foreach (var id in ids)
        {
            bulkDescriptor.Delete<Product>(op => op.Id(id));
        }
        var response = await _elasticClient.BulkAsync(bulkDescriptor);
        if (!response.IsValid)
            throw new Exception($"Failed to bulk delete products: {response.OriginalException.Message}");
    }
}