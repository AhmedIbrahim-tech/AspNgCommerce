namespace ECommerce.Core.Interfaces.Repository;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetListOfProductsAsync();
    Task<Product> GetProductByIDAsync(int id);
}
