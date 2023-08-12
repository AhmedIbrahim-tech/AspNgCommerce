using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private readonly IConnectionMultiplexer redis;
    private readonly IGenericRepository<Product> _GenericProductRepository;
    private readonly IGenericRepository<ProductBrand> _GenericProductBrandRepository;
    private readonly IGenericRepository<ProductType> _GenericProductTypeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductsRepository _productsRepository;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
        this.redis = redis;
    }
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
    public IProductsRepository ProductsRepository => _productsRepository ?? new ProductsRepository(_context);

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

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}