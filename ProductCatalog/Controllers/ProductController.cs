using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Retrieves all products (v1).
    /// </summary>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);
        return Ok(products);
    }

    /// <summary>
    /// Retrieves paginated list of products (v2).
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A paginated list of products.</returns>
    [HttpGet]
    [MapToApiVersion("2.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetProductsPagedAsync(page, pageSize, cancellationToken);
        return Ok(products);
    }

    /// <summary>
    /// Retrieves a single product by ID (v1 and v2).
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The product details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }
        return Ok(product);
    }

    /// <summary>
    /// Updates the description of a product (v1 and v2).
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="description">The new description for the product.</param>
    /// <returns>No content if successful.</returns>
    [HttpPatch("{id}/description")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductDescription(int id, [FromBody] string description, CancellationToken cancellationToken)
        {
        if (string.IsNullOrEmpty(description))
        {
            return BadRequest("Description cannot be null or empty.");
        }

        var updated = await _productService.UpdateProductDescriptionAsync(id, description, cancellationToken);
        if (!updated)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        return NoContent();
    }
}
