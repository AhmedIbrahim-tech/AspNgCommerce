namespace ECommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : BaseAPIController
{
    private readonly IProductServices productServices;

    public ProductController(IProductServices productServices)
    {
        this.productServices = productServices;
    }

    /// <summary>
    /// Get All Products By Use Specification Pattern
    /// </summary>
    /// <param name="productSpecParams"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductSpecParams productSpecParams)
    {
        var result = await productServices.GetAllProductsAsync(productSpecParams);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("ProductTypes")]
    public async Task<IActionResult> GetProductTypesAsync()
    {
        var result = await productServices.GetProductTypesAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("ProductBrands")]
    public async Task<IActionResult> GetProductBrandsAsync()
    {
        var result = await productServices.GetProductBrandsAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await productServices.GetProductByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}
