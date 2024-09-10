using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDBContext _context;

    public ProductRepository(ApplicationDBContext context)
    {
        this._context = context;
    }

    #region Get Product Brands
    public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
    {
        return await _context.ProductBrands.ToListAsync();
    }
    #endregion

    #region Get Product By Id
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(x => x.ProductBrand)
            .Include(x => x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    #endregion

    #region Get All Products
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(x => x.ProductBrand)
            .Include(x => x.ProductType)
            .ToListAsync();
    }
    #endregion

    #region Get Product Types
    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
        return await _context.ProductTypes.ToListAsync();
    }
    #endregion

    #region Get Products With Spec
    public async Task<IReadOnlyList<Product>> ListAsync(ISpecification<Product> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<Product> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    private IQueryable<Product> ApplySpecification(ISpecification<Product> spec)
    {
        return SpecificationEvaluator<Product>.GetQuery(_context.Products.AsQueryable(), spec);
    }
    #endregion

}
