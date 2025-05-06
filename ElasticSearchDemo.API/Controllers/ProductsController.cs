using ElasticSearchDemo.API.Models;
using ElasticSearchDemo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        try
        {
            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to create product: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to get product: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to get all products: {ex.Message}");
        }
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        try
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to get products by category: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm)
    {
        try
        {
            var products = await _productService.SearchProductsAsync(searchTerm);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to search products: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        try
        {
            await _productService.UpdateProductAsync(product);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to update product: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to delete product: {ex.Message}");
        }
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkCreateProducts([FromBody] IEnumerable<Product> products)
    {
        try
        {
            await _productService.BulkCreateProductsAsync(products);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to bulk create products: {ex.Message}");
        }
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdateProducts([FromBody] IEnumerable<Product> products)
    {
        try
        {
            await _productService.BulkUpdateProductsAsync(products);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to bulk update products: {ex.Message}");
        }
    }

    [HttpDelete("bulk")]
    public async Task<IActionResult> BulkDeleteProducts([FromBody] IEnumerable<string> ids)
    {
        try
        {
            await _productService.BulkDeleteProductsAsync(ids);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to bulk delete products: {ex.Message}");
        }
    }
}