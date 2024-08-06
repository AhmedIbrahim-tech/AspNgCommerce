namespace ECommerce.Infrastrucure.Repositories;

public interface IProductBrandRepository : IGenericRepository<ProductBrand>
{
    // Additional methods specific to ProductBrand can be added here if needed
}


public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
{
    private readonly ApplicationDBContext _context;

    public ProductBrandRepository(ApplicationDBContext context) : base(context)
    {
        _context = context;
    }
}
