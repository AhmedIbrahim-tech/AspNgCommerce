namespace ECommerce.Core.Interfaces.Services;
public interface IProductServices
{
    Task<BaseGenericResult<Pagination<ProductDTo>>> GetAllProductsAsync(ProductSpecParams productSpecParams);
    Task<BaseGenericResult<ProductDTo>> GetProductByIdAsync(int id);
    Task<BaseGenericResult<IReadOnlyList<ProductType>>> GetProductTypesAsync();
    Task<BaseGenericResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync();
}
