using System.Net;

namespace ECommerce.Core.Services;

public class ProductServices : IProductServices
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductServices(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<BaseGenericResult<IReadOnlyList<Product>>> GetAllProductsAsync()
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductsAsync();
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", result);

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<BaseGenericResult<Product>> GetProductByIdAsync(int id)
    {
        try
        {
            var result = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", result);

        }
        catch (Exception ex)
        {
            return new(false, (int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

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
}
