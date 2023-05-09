using Newtonsoft.Json;

namespace ECommerce.API.Controllers;

#region Contractor

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsServices productServices;
    private readonly IMapper _mapper;

    public ProductsController(IProductsServices productServices, IMapper mapper)
    {
        this.productServices = productServices;
        _mapper = mapper;
    }

    #endregion

    #region Get List Of Products

    [HttpPost]
    public async Task<IActionResult> GetListOfProducts([FromBody] ProductsQueryFilter filter)
    {
        var result = await productServices.GetListOfProductsAsync(filter);
        var Dto = _mapper.Map<IEnumerable<ProductDto>>(result.Data);
     
        #region Meta data

        var metadata = new Metadata
        {
            TotalCount = result.Data.TotalCount,
            PageSize = result.Data.PageSize,
            CurrentPage = result.Data.CurrentPage,
            TotalPages = result.Data.TotalPages,
            HasNextPage = result.Data.HasNextPage,
            HasPreviousPage = result.Data.HasPreviousPage,
        };
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        #endregion
        
        var response = new BaseGenericResult<IEnumerable<ProductDto>>(result.Status , result.StatusCode , result.Message , Dto) { Meta = metadata};

        return StatusCode(response.StatusCode, response);
    }

    #endregion

    #region Get Single Of Products

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIDAsync(int id)
    {
        var result = await productServices.GetProductByIDAsync(id);
       
        var resultDto = _mapper.Map<ProductDto>(result.Data);

        var response = new BaseGenericResult<ProductDto>(result.Status, result.StatusCode, result.Message, resultDto);

        return StatusCode(response.StatusCode, response);
    }

    #endregion


}
