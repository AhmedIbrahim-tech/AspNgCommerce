using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Repositories;


public interface IProductsRepository
{
    //Task<IEnumerable<Product>> GetListOfProductsAsync();
    IQueryable<Product> GetQueryable();
    Task<Product> GetProductByIDAsync(int id);
    Task AddProductAsync(Product product);
    void UpdateProduct(Product product);
    Task DeleteProductAsync(int id);
}

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDBContext _context;
    public ProductsRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIDAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Product> GetQueryable()
    {
        return _context.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType);
    }
    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await GetProductByIDAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
    }


}
