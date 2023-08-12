using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        this._context = context;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();

    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products.Include(x => x.ProductBrand).Include(x => x.ProductType).ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();

    }
}
