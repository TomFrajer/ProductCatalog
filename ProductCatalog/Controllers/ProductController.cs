using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services;

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
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
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
    [Route("paged")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var products = await _productService.GetProductsPagedAsync(page, pageSize);
        return Ok(products);
    }
}
