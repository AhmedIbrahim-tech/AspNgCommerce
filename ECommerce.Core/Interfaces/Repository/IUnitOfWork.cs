namespace ECommerce.Core.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }
    IGenericRepository<Product> GenericRepository { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}
