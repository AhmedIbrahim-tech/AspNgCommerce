namespace ECommerce.Core.Interfaces.Services;

public interface IProductServices
{
    Task<BaseGenericResult<IReadOnlyList<ProductDto>>> GetAllProductsAsync();
    Task<BaseGenericResult<ProductDto>> GetProductByIdAsync(int id);
    Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync();
    Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync();
}
