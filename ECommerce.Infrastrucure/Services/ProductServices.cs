using AutoMapper;
using ECommerce.Core.CustomEntities;
using ECommerce.Core.DTOS;
using ECommerce.Core.Responses;

namespace ECommerce.Infrastrucure.Services
{
    public class ProductServices : BaseGenericResultHandler, IProductServices
    {
        #region Contractor(s)
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Get All Products
        public async Task<BaseGenericResult<Pagination<ProductDto>>> GetAllProductsAsync(ProductSpecParams productSpecParams)
        {
            try
            {
                var spec = new ProductWithTypesAndBrandsSpecification(productSpecParams);
                var countSpec = new ProductWithFiltersForCountSpecification(productSpecParams);
                var totalItems = await _unitOfWork.GenericProductRepository.CountAsync(countSpec);

                var result = await _unitOfWork.GenericProductRepository.ListAsync(spec);
                var listDto = _mapper.Map<IReadOnlyList<ProductDto>>(result);

                var pageList = new Pagination<ProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, listDto);

                return Success(pageList);
            }
            catch (Exception ex)
            {
                return InternalServer<Pagination<ProductDto>>(ex.Message);
            }
        }
        #endregion

        #region Get Product By Id
        public async Task<BaseGenericResult<ProductDto>> GetProductByIdAsync(int id)
        {
            try
            {
                var spec = new ProductWithTypesAndBrandsSpecification(id);
                var result = await _unitOfWork.GenericProductRepository.GetEntityWithSpec(spec);
                var dto = _mapper.Map<ProductDto>(result);
                return Success(dto);
            }
            catch (Exception ex)
            {
                return InternalServer<ProductDto>(ex.Message);
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
}
