using ECommerce.Core.CustomEntities;
using ECommerce.Core.Interfaces.Specifications;
using System.Net;

namespace ECommerce.Core.Services;

public class ProductServices : IProductServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Get All Products
    public async Task<BaseGenericResult<IReadOnlyList<ProductDto>>> GetAllProductsAsync()
    {
        try
        {
            var Spec = new ProductWithTypesAndBrandsSpecification();
            var result = await _unitOfWork.GProductRepository.ListAsync(Spec);
            var ListDto = _mapper.Map<IReadOnlyList<ProductDto>>(result);
            var metadata = new Metadata
            {
                //TotalCount = result.TotalCount,
                //PageSize = result.PageSize,
                //CurrentPage = result.CurrentPage,
                //TotalPages = result.TotalPages,
                //HasNextPage = result.HasNextPage,
                ////HasPreviousPage = result.HasPreviousPage,
            };
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", ListDto)
            {
                Meta = metadata
            };

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Get Product By Id
    public async Task<BaseGenericResult<ProductDto>> GetProductByIdAsync(int id)
    {
        try
        {
            var Spec = new ProductWithTypesAndBrandsSpecification(id);
            var result = await _unitOfWork.GProductRepository.GetEntityWithSpec(Spec);
            var Dto = _mapper.Map<ProductDto>(result);
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", Dto);

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    #endregion
    
    #region Get Product Types
    public async Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductTypesAsync();
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", result);

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    #endregion

    #region Get Product Brands
    public async Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductBrandsAsync();
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", result);

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    #endregion


}
