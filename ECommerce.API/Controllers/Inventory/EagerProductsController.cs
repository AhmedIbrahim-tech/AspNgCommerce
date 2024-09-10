using ECommerce.Infrastrucure.Services;

[ApiController]
public class EagerProductsController : ControllerBase
{
    #region Constructor
    private readonly IProductsServices _productsServices;

    public EagerProductsController(IProductsServices productsServices)
    {
        _productsServices = productsServices;
    }
    #endregion

    #region Handle of Functions

    #region Get List Of Products
    [HttpGet(Router.EagerProducts.ListProduct)]
    public async Task<IActionResult> GetProducts([FromQuery] ProductsQueryFilter filter)
    {
        var result = await _productsServices.GetListOfProductsAsync(filter);
        if (result.Status)
        {
            return StatusCode(result.StatusCode, result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Get Single Product
    [HttpGet(Router.EagerProducts.GetById)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _productsServices.GetProductByIDAsync(id);
        if (result.Status)
        {
            return StatusCode(result.StatusCode, result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Create Product
    [HttpPost(Router.EagerProducts.Create)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productsServices.AddProductAsync(productDto);
        if (result.Status)
        {
            return StatusCode(result.StatusCode, result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Update Product
    [HttpPut(Router.EagerProducts.Edit)]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _productsServices.UpdateProductAsync(id, productDto);
        if (result.Status)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }
    #endregion

    #region Delete Product
    [HttpDelete(Router.EagerProducts.Delete)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productsServices.DeleteProductAsync(id);
        if (result.Status)
        {
            return StatusCode(result.StatusCode, result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #endregion
}
