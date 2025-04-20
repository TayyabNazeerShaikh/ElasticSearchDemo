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
        await _productService.CreateProductAsync(product);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        await _productService.UpdateProductAsync(product);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok();
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkCreateProducts([FromBody] IEnumerable<Product> products)
    {
        await _productService.BulkCreateProductsAsync(products);
        return Ok();
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdateProducts([FromBody] IEnumerable<Product> products)
    {
        await _productService.BulkUpdateProductsAsync(products);
        return Ok();
    }

    [HttpDelete("bulk")]
    public async Task<IActionResult> BulkDeleteProducts([FromBody] IEnumerable<string> ids)
    {
        await _productService.BulkDeleteProductsAsync(ids);
        return Ok();
    }
}