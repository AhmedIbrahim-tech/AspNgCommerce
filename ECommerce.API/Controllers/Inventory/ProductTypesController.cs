using ECommerce.Infrastrucure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Inventory;

[ApiController]
public class ProductTypesController : ControllerBase
{
    private readonly IProductTypeService _productTypeService;

    public ProductTypesController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }

    #region Get All Product Types
    [HttpGet(Router.ProductTypes.ListProductTypes)]
    public async Task<IActionResult> GetAllProductTypes()
    {
        var result = await _productTypeService.GetAllProductTypesAsync();
        if (result.Status)
        {
            return Ok(result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Get Product Type by ID
    [HttpGet(Router.ProductTypes.GetById)]
    public async Task<IActionResult> GetProductTypeById(int id)
    {
        var result = await _productTypeService.GetProductTypeByIdAsync(id);
        if (result.Status)
        {
            return Ok(result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Create Product Type
    [HttpPost(Router.ProductTypes.Create)]
    public async Task<IActionResult> CreateProductType([FromBody] ProductType productType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productTypeService.AddProductTypeAsync(productType);
        if (result.Status)
        {
            return CreatedAtAction(nameof(GetProductTypeById), new { id = result.Data.Id }, result.Data);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Update Product Type
    [HttpPut(Router.ProductTypes.Edit)]
    public async Task<IActionResult> UpdateProductType(int id, [FromBody] ProductType productType)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productTypeService.UpdateProductTypeAsync(id, productType);
        if (result.Status)
        {
            return NoContent();
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Delete Product Type
    [HttpDelete(Router.ProductTypes.Delete)]
    public async Task<IActionResult> DeleteProductType(int id)
    {
        var result = await _productTypeService.DeleteProductTypeAsync(id);
        if (result.Status)
        {
            return NoContent();
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion
}
