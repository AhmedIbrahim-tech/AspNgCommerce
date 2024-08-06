namespace ECommerce.Core.Interfaces.Services;
public interface IProductServices
{
    Task<BaseGenericResult<Pagination<ProductDto>>> GetAllProductsAsync(ProductSpecParams productSpecParams);
    Task<BaseGenericResult<ProductDto>> GetProductByIdAsync(int id);
    Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync();
    Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync();
}
