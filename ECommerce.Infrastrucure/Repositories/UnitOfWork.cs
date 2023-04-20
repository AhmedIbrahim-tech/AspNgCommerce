namespace ECommerce.Infrastrucure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private readonly IGenericRepository<Product> g_ProductRepository;
    private readonly IGenericRepository<ProductBrand> g_ProductBrandRepository;
    private readonly IGenericRepository<ProductType> g_ProductTypeRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductsRepository _productsRepository;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
    }
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
    public IProductsRepository ProductsRepository => _productsRepository ?? new ProductsRepository(_context);

    public IGenericRepository<Product> GProductRepository => g_ProductRepository ?? new GenericRepository<Product>(_context);

    public IGenericRepository<ProductBrand> GProductBrandRepository => g_ProductBrandRepository ?? new GenericRepository<ProductBrand>(_context);

    public IGenericRepository<ProductType> GProductTypeRepository => g_ProductTypeRepository ?? new GenericRepository<ProductType>(_context);

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

