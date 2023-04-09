namespace ECommerce.Core.Interfaces.Services;

public interface IProductServices
{
    Task<BaseGenericResult<IReadOnlyList<Product>>> GetAllProductsAsync();
    Task<BaseGenericResult<Product>> GetProductByIdAsync(int id);
    Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync();
    Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync();
}
