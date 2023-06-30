namespace ECommerce.Infrastrucure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDBContext _context;
    public ProductsRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetListOfProductsAsync()
    {
        return await _context.Products.Include(x => x.ProductType).Include(x => x.ProductBrand).ToListAsync();
    }

    public async Task<Product> GetProductByIDAsync(int id)
    {
        return await _context.Products.Include(x => x.ProductType).Include(x => x.ProductBrand).FirstOrDefaultAsync(x => x.Id == id);
    }
}
