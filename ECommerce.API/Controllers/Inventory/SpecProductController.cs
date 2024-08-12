#region Fields
using ECommerce.API.Controllers.APIControllers;

namespace ECommerce.API.Controllers.Inventory;

[ApiController]

public class SpecProductController : BaseAPIController
{
    #endregion

    #region Contractor (s)

    private readonly IProductServices productServices;
    public SpecProductController(IProductServices productServices)
    {
        this.productServices = productServices;
    }

    #endregion

    #region Handle of Function (s)

    #region Display List of Product With Filters
    [HttpGet(Router.SpecProduct.ListProduct)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductSpecParams productSpecPrams)
    {
        var result = await productServices.GetAllProductsAsync(productSpecPrams);
        return StatusCode(result.StatusCode, result);
    }
    #endregion

    #region Display List of Product Types
    [HttpGet(Router.SpecProduct.ProductTypes)]
    public async Task<IActionResult> GetProductTypesAsync()
    {
        var result = await productServices.GetProductTypesAsync();
        return StatusCode(result.StatusCode, result);
    }
    #endregion

    #region Display List of Product Brands
    [HttpGet(Router.SpecProduct.ProductBrands)]
    public async Task<IActionResult> GetProductBrandsAsync()
    {
        var result = await productServices.GetProductBrandsAsync();
        return StatusCode(result.StatusCode, result);
    }
    #endregion

    #region Display One Product By Using Id
    [HttpGet(Router.SpecProduct.GetById)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await productServices.GetProductByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }
    #endregion 

}
#endregion
