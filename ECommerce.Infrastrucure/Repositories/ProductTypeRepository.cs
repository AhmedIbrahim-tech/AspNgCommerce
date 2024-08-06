namespace ECommerce.Infrastrucure.Repositories;

public interface IProductTypeRepository : IGenericRepository<ProductType>
{
    // Additional methods specific to ProductType can be added here if needed
}

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    private readonly ApplicationDBContext _context;

    public ProductTypeRepository(ApplicationDBContext context) : base(context)
    {
        _context = context;
    }
}
