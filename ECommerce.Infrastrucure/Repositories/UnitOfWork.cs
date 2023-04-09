namespace ECommerce.Infrastrucure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;
    private readonly IGenericRepository<Product> genericRepository;
    private readonly IProductRepository _productRepository;

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;
    }
    public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

    public IGenericRepository<Product> GenericRepository => genericRepository ?? new GenericRepository<Product>(_context);



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

