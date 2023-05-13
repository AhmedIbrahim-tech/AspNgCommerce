namespace ECommerce.Core.Interfaces.Services;

public interface IProductsServices
{
    Task<BaseGenericResult<PagedList<Product>>> GetListOfProductsAsync(ProductsQueryFilter filter);
    Task<BaseGenericResult<Product>> GetProductByIDAsync(int id);

}
