using AutoMapper;
using ECommerce.Core.CustomEntities;
using ECommerce.Core.DTOS;
using ECommerce.Core.QueryFilters;
using ECommerce.Core.Responses;
using System.Net;

namespace ECommerce.Infrastrucure.Services;


#region Interface
public interface IProductsServices
{
    Task<BaseGenericResult<PagedList<ProductDto>>> GetListOfProductsAsync(ProductsQueryFilter filter);
    Task<BaseGenericResult<ProductDto>> GetProductByIDAsync(int id);
    Task<BaseGenericResult<ProductDto>> AddProductAsync(ProductDto productDto);
    Task<BaseGenericResult<ProductDto>> UpdateProductAsync(int id, ProductDto productDto);
    Task<BaseGenericResult<int>> DeleteProductAsync(int id);

}
#endregion


public class ProductsServices : IProductsServices
{
    #region Contractor

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion

    #region Get List Of Products

    public async Task<BaseGenericResult<PagedList<ProductDto>>> GetListOfProductsAsync(ProductsQueryFilter filter)
    {
        try
        {
            var productsQuery = _unitOfWork.ProductsRepository.GetQueryable();

            // Filtration
            if (filter.BrandId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.ProductBrandID == filter.BrandId.Value);
            }

            if (filter.TypeId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.ProductTypeID == filter.TypeId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                productsQuery = productsQuery.Where(x => x.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                productsQuery = productsQuery.Where(x => x.Description.Contains(filter.Description, StringComparison.OrdinalIgnoreCase));
            }

            // Pagination
            filter.PageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            filter.PageSize = filter.PageSize <= 0 ? 10 : filter.PageSize;

            var pagedProducts = await PagedList<Product>.CreateAsync(productsQuery, filter.PageNumber, filter.PageSize);

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(pagedProducts).ToList();
            return new(true, (int)HttpStatusCode.OK, "Data Loaded Successfully", new PagedList<ProductDto>(productDtos, pagedProducts.TotalCount, pagedProducts.CurrentPage, pagedProducts.PageSize));
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "An error occurred while loading the product list.");

            return new(false, (int)HttpStatusCode.InternalServerError, "Data Loading Failed → " + ex.Message);
        }
    }

    #endregion

    #region Get Single Product

    public async Task<BaseGenericResult<ProductDto>> GetProductByIDAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductsRepository.GetProductByIDAsync(id);

            if (product == null)
                return new(false, (int)HttpStatusCode.NotFound, "Product not found.");

            var ProductDto = _mapper.Map<ProductDto>(product);

            return new(true, (int)HttpStatusCode.OK, "Data Loaded Successfully", ProductDto);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "An error occurred while loading the product with ID {ProductId}.", id);

            return new(false, (int)HttpStatusCode.InternalServerError, "Data Loading Failed → " + ex.Message);
        }
    }

    #endregion

    #region Add Product

    public async Task<BaseGenericResult<ProductDto>> AddProductAsync(ProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.ProductsRepository.AddProductAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var addedProductDto = _mapper.Map<ProductDto>(product);

            return new(true, (int)HttpStatusCode.Created, "Product Added Successfully", addedProductDto);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "An error occurred while adding a new product.");

            return new(false, (int)HttpStatusCode.InternalServerError, "Adding Product Failed → " + ex.Message);
        }
    }


    #endregion

    #region Update Product

    public async Task<BaseGenericResult<ProductDto>> UpdateProductAsync(int id, ProductDto ProductDto)
    {
        try
        {
            var product = await _unitOfWork.ProductsRepository.GetProductByIDAsync(id);
            if (product == null)
                return new(false, (int)HttpStatusCode.NotFound, "Product not found.");

            _mapper.Map(ProductDto, product);
            _unitOfWork.ProductsRepository.UpdateProduct(product);
            await _unitOfWork.SaveChangesAsync();

            var updatedProductDto = _mapper.Map<ProductDto>(product);

            return new(true, (int)HttpStatusCode.OK, "Product Updated Successfully", updatedProductDto);
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "An error occurred while updating the product with ID {ProductId}.", id);

            return new(false, (int)HttpStatusCode.InternalServerError, "Updating Product Failed → " + ex.Message);
        }
    }

    #endregion

    #region Delete Product

    public async Task<BaseGenericResult<int>> DeleteProductAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductsRepository.GetProductByIDAsync(id);
            if (product == null)
                return new(false, (int)HttpStatusCode.NotFound, "Product not found.");

            await _unitOfWork.ProductsRepository.DeleteProductAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return new(true, (int)HttpStatusCode.OK, "Product Deleted Successfully");
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "An error occurred while deleting the product with ID {ProductId}.", id);

            return new(false, (int)HttpStatusCode.InternalServerError, "Deleting Product Failed → " + ex.Message);
        }
    }

    #endregion


}
