using ECommerce.Core.Interfaces.UnitOfWork;

namespace ECommerce.Core.Services;

public class ProductServices : BaseGenericResultHandler, IProductServices
{
    #region Contractor (s) 
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    #endregion

    #region Get All Products
    public async Task<BaseGenericResult<Pagination<ProductDTo>>> GetAllProductsAsync(ProductSpecParams productSpecParams)
    {
        try
        {
            var Spec = new ProductWithTypesAndBrandsSpecification(productSpecParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);
            var totalItems = await _unitOfWork.GenericProductRepository.CountAsync(countSpec);

            var result = await _unitOfWork.GenericProductRepository.ListAsync(Spec);
            var ListDto = _mapper.Map<IReadOnlyList<ProductDTo>>(result);

            var pageList = new Pagination<ProductDTo>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, ListDto);

            return Success(pageList);

        }
        catch (Exception ex)
        {
            return InternalServer<Pagination<ProductDTo>>(ex.Message);
        }
    }
    #endregion

    #region Get Product By Id
    public async Task<BaseGenericResult<ProductDTo>> GetProductByIdAsync(int id)
    {
        try
        {
            var Spec = new ProductWithTypesAndBrandsSpecification(id);
            var result = await _unitOfWork.GenericProductRepository.GetEntityWithSpec(Spec);
            var Dto = _mapper.Map<ProductDTo>(result);
            return Success(Dto);

        }
        catch (Exception ex)
        {
            return InternalServer<ProductDTo>(ex.Message);
        }
    }
    #endregion

    #region Get Product Types
    public async Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductTypesAsync();
            return Success(result);

        }
        catch (Exception ex)
        {
            return InternalServer<IReadOnlyList<ProductType>>(ex.Message);
        }
    }
    #endregion

    #region Get Product Brands
    public async Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductBrandsAsync();
            return Success(result);

        }
        catch (Exception ex)
        {
            return InternalServer<IReadOnlyList<ProductBrand>>(ex.Message);
        }
    }
    #endregion

}
