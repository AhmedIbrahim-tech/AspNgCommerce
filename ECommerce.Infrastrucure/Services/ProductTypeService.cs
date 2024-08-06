using ECommerce.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastrucure.Services;

public interface IProductTypeService
{
    Task<BaseGenericResult<ProductType>> GetProductTypeByIdAsync(int id);
    Task<BaseGenericResult<IEnumerable<ProductType>>> GetAllProductTypesAsync();
    Task<BaseGenericResult<ProductType>> AddProductTypeAsync(ProductType productType);
    Task<BaseGenericResult<ProductType>> UpdateProductTypeAsync(int id, ProductType productType);
    Task<BaseGenericResult<int>> DeleteProductTypeAsync(int id);
}

public class ProductTypeService : IProductTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseGenericResult<ProductType>> GetProductTypeByIdAsync(int id)
    {
        try
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType == null)
                return new BaseGenericResult<ProductType>(false, (int)HttpStatusCode.NotFound, "Product type not found.");

            return new BaseGenericResult<ProductType>(true, (int)HttpStatusCode.OK, "Product type loaded successfully.", productType);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductType>(false, (int)HttpStatusCode.InternalServerError, "Loading product type failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<IEnumerable<ProductType>>> GetAllProductTypesAsync()
    {
        try
        {
            var productTypes = await _unitOfWork.ProductTypeRepository.ListAllAsync();
            return new BaseGenericResult<IEnumerable<ProductType>>(true, (int)HttpStatusCode.OK, "Product types loaded successfully.", productTypes);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<IEnumerable<ProductType>>(false, (int)HttpStatusCode.InternalServerError, "Loading product types failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<ProductType>> AddProductTypeAsync(ProductType productType)
    {
        try
        {
            await _unitOfWork.ProductTypeRepository.AddAsync(productType);
            await _unitOfWork.SaveChangesAsync();

            return new BaseGenericResult<ProductType>(true, (int)HttpStatusCode.Created, "Product type added successfully.", productType);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductType>(false, (int)HttpStatusCode.InternalServerError, "Adding product type failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<ProductType>> UpdateProductTypeAsync(int id, ProductType productType)
    {
        try
        {
            var existingProductType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (existingProductType == null)
                return new BaseGenericResult<ProductType>(false, (int)HttpStatusCode.NotFound, "Product type not found.");

            existingProductType.Name = productType.Name;

            await _unitOfWork.ProductTypeRepository.EditAsync(existingProductType);
            await _unitOfWork.SaveChangesAsync();

            return new BaseGenericResult<ProductType>(true, (int)HttpStatusCode.OK, "Product type updated successfully.", existingProductType);
        }
        catch (Exception ex)
        {
            return new BaseGenericResult<ProductType>(false, (int)HttpStatusCode.InternalServerError, "Updating product type failed → " + ex.Message);
        }
    }

    public async Task<BaseGenericResult<int>> DeleteProductTypeAsync(int id)
    {
        try
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType == null)
                return new (false, (int)HttpStatusCode.NotFound, "Product type not found.");

            await _unitOfWork.ProductTypeRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return new (true, (int)HttpStatusCode.OK, "Product type deleted successfully.");
        }
        catch (Exception ex)
        {
            return new (false, (int)HttpStatusCode.InternalServerError, "Deleting product type failed → " + ex.Message);
        }
    }
}

