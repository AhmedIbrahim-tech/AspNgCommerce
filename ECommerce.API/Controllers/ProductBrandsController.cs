using ECommerce.Infrastrucure.Services;

namespace ECommerce.API.Controllers;

[ApiController]
public class ProductBrandsController : ControllerBase
{
    private readonly IProductBrandService _productBrandService;

    public ProductBrandsController(IProductBrandService productBrandService)
    {
        _productBrandService = productBrandService;
    }

    #region Get All Product Brands
    [HttpGet(Router.ProductBrands.ListProductBrands)]
    public async Task<IActionResult> GetAllProductBrands()
    {
        var result = await _productBrandService.GetAllProductBrandsAsync();
        if (result.Status)
        {
            return Ok(result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Get Product Brand by ID
    [HttpGet(Router.ProductBrands.GetById)]
    public async Task<IActionResult> GetProductBrandById(int id)
    {
        var result = await _productBrandService.GetProductBrandByIdAsync(id);
        if (result.Status)
        {
            return Ok(result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Create Product Brand
    [HttpPost(Router.ProductBrands.Create)]
    public async Task<IActionResult> CreateProductBrand([FromBody] ProductBrand productBrand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productBrandService.AddProductBrandAsync(productBrand);
        if (result.Status)
        {
            return CreatedAtAction(nameof(GetProductBrandById), new { id = result.Data.Id }, result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Update Product Brand
    [HttpPut(Router.ProductBrands.Edit)]
    public async Task<IActionResult> UpdateProductBrand(int id, [FromBody] ProductBrand productBrand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productBrandService.UpdateProductBrandAsync(id, productBrand);
        if (result.Status)
        {
            return NoContent();
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Delete Product Brand
    [HttpDelete(Router.ProductBrands.Delete)]
    public async Task<IActionResult> DeleteProductBrand(int id)
    {
        var result = await _productBrandService.DeleteProductBrandAsync(id);
        if (result.Status)
        {
            return NoContent();
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion
}