using Microsoft.AspNetCore.Mvc;

using WebStore.Domain;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
    private readonly IProductData _ProductData;
    private readonly ILogger<ProductsApiController> _Logger;

    public ProductsApiController(IProductData ProductData, ILogger<ProductsApiController> Logger)
    {
        _ProductData = ProductData;
        _Logger = Logger;
    }

    [HttpGet("sections")]
    public IActionResult GetSections() => Ok(_ProductData.GetSections());

    [HttpGet("sections/{Id:int}")]
    public IActionResult GetSectionById(int Id) => _ProductData.GetSectionById(Id) is { } section
        ? Ok(section)
        : NotFound(new { Id });

    [HttpGet("brands")]
    public IActionResult GetBrands() => Ok(_ProductData.GetBrands());

    [HttpGet("brands/{Id:int}")]
    public IActionResult GetBrandById(int Id) => _ProductData.GetBrandById(Id) is { } brand
        ? Ok(brand)
        : NotFound(new { Id });

    [HttpPost]
    public IActionResult GetProducts([FromBody] ProductFilter filter)
    {
        var products = _ProductData.GetProducts(filter);

        if (products.Any())
            return Ok(products);
        return NoContent();
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetProductById(int Id) => _ProductData.GetProductById(Id) is { } product
        ? Ok(product)
        : NoContent();
}
