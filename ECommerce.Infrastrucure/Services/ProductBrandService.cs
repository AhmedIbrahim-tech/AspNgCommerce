using ECommerce.Core.Responses;
using System.Net;

namespace ECommerce.Infrastrucure.Services;

public interface IProductBrandService
{
    Task<BaseGenericResult<ProductBrand>> GetProductBrandByIdAsync(int id);
    Task<BaseGenericResult<IEnumerable<ProductBrand>>> GetAllProductBrandsAsync();
    Task<BaseGenericResult<ProductBrand>> AddProductBrandAsync(ProductBrand productBrand);
    Task<BaseGenericResult<ProductBrand>> UpdateProductBrandAsync(int id, ProductBrand productBrand);
    Task<BaseGenericResult<int>> DeleteProductBrandAsync(int id);
}



public class ProductBrandService : IProductBrandService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductBrandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseGenericResult<ProductBrand>> GetProductBrandByIdAsync(int id)
    {
        try
        {
            var productBrand = await _unitOfWork.ProductBrandRepository.GetByIdAsync(id);
            if (productBrand == null)
                return new BaseGenericResult<ProductBrand>(false, (int)HttpStatusCode.NotFound, "Product brand not found.");

            return new BaseGenericResult<ProductBrand>(true, (int)HttpStatusCode.OK, "Product brand loaded successfully.", productBrand);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductBrand>(false, (int)HttpStatusCode.InternalServerError, "Loading product brand failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<IEnumerable<ProductBrand>>> GetAllProductBrandsAsync()
    {
        try
        {
            var productBrands = await _unitOfWork.ProductBrandRepository.ListAllAsync();
            return new BaseGenericResult<IEnumerable<ProductBrand>>(true, (int)HttpStatusCode.OK, "Product brands loaded successfully.", productBrands);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<IEnumerable<ProductBrand>>(false, (int)HttpStatusCode.InternalServerError, "Loading product brands failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<ProductBrand>> AddProductBrandAsync(ProductBrand productBrand)
    {
        try
        {
            await _unitOfWork.ProductBrandRepository.AddAsync(productBrand);
            await _unitOfWork.SaveChangesAsync();

            return new BaseGenericResult<ProductBrand>(true, (int)HttpStatusCode.Created, "Product brand added successfully.", productBrand);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductBrand>(false, (int)HttpStatusCode.InternalServerError, "Adding product brand failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<ProductBrand>> UpdateProductBrandAsync(int id, ProductBrand productBrand)
    {
        try
        {
            var existingProductBrand = await _unitOfWork.ProductBrandRepository.GetByIdAsync(id);
            if (existingProductBrand == null)
                return new BaseGenericResult<ProductBrand>(false, (int)HttpStatusCode.NotFound, "Product brand not found.");

            existingProductBrand.Name = productBrand.Name;

            _unitOfWork.ProductBrandRepository.EditAsync(existingProductBrand);
            await _unitOfWork.SaveChangesAsync();

            return new BaseGenericResult<ProductBrand>(true, (int)HttpStatusCode.OK, "Product brand updated successfully.", existingProductBrand);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductBrand>(false, (int)HttpStatusCode.InternalServerError, "Updating product brand failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<int>> DeleteProductBrandAsync(int id)
    {
        try
        {
            var productBrand = await _unitOfWork.ProductBrandRepository.GetByIdAsync(id);
            if (productBrand == null)
                return new (false, (int)HttpStatusCode.NotFound, "Product brand not found.");

            await _unitOfWork.ProductBrandRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return new (true, (int)HttpStatusCode.OK, "Product brand deleted successfully.");
        }
        catch (Exception ex)
        {
            return new (false, (int)HttpStatusCode.InternalServerError, "Deleting product brand failed → " + ex.Message);
        }
    }
}
