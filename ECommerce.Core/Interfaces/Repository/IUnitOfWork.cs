namespace ECommerce.Core.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IGenericRepository<Product> GProductRepository { get; }
    IGenericRepository<ProductBrand> GProductBrandRepository { get; }
    IGenericRepository<ProductType> GProductTypeRepository { get; }

    IProductsRepository ProductsRepository { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}
