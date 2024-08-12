using ECommerce.Infrastrucure.Repositories.Permissions;
using System.Collections;

namespace ECommerce.Infrastrucure.Repositories;
public interface IUnitOfWork : IDisposable
{
    // Generic Repository Pattern
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    IGenericRepository<Product> GenericProductRepository { get; }
    IGenericRepository<ProductBrand> GenericProductBrandRepository { get; }
    IGenericRepository<ProductType> GenericProductTypeRepository { get; }

    // Specification Pattern
    IProductRepository ProductRepository { get; }

    // Repository Pattern
    IProductsRepository ProductsRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductTypeRepository ProductTypeRepository { get; }
    IProductBrandRepository ProductBrandRepository { get; }
    IPermissionsRepository PermissionsRepository { get; }

    void SaveChanges();
    Task<int> SaveChangesAsync();
}


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private Hashtable _repositories;
    private readonly IConnectionMultiplexer redis;
    private readonly IGenericRepository<Product> _GenericProductRepository;
    private readonly IGenericRepository<ProductBrand> _GenericProductBrandRepository;
    private readonly IGenericRepository<ProductType> _GenericProductTypeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly ICategoryRepository _categoryRepository;
    private IProductTypeRepository _productTypeRepository;
    private IProductBrandRepository _productBrandRepository;
    private IPermissionsRepository _permissionsRepository;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        this.redis = redis;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
    public IProductsRepository ProductsRepository => _productsRepository ?? new ProductsRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
    public IProductTypeRepository ProductTypeRepository => _productTypeRepository ??= new ProductTypeRepository(_context);
    public IProductBrandRepository ProductBrandRepository => _productBrandRepository ??= new ProductBrandRepository(_context);
    public IPermissionsRepository PermissionsRepository => _permissionsRepository ??= new PermissionsRepository(_context);

    public IGenericRepository<Product> GenericProductRepository => _GenericProductRepository ?? new GenericRepository<Product>(_context);
    public IGenericRepository<ProductBrand> GenericProductBrandRepository => _GenericProductBrandRepository ?? new GenericRepository<ProductBrand>(_context);
    public IGenericRepository<ProductType> GenericProductTypeRepository => _GenericProductTypeRepository ?? new GenericRepository<ProductType>(_context);


    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}