namespace ECommerce.Core.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // Generic Repository Pattern
    IGenericRepository<Product> GenericProductRepository { get; }
    IGenericRepository<ProductBrand> GenericProductBrandRepository { get; }
    IGenericRepository<ProductType> GenericProductTypeRepository { get; }

    // Specification Pattern
    IProductRepository ProductRepository { get; }

    // Repository Pattern
    IProductsRepository ProductsRepository { get; }


    void SaveChanges();
    Task SaveChangesAsync();
}
