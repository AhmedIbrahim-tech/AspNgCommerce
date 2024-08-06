namespace ECommerce.Infrastrucure.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly ApplicationDBContext _context;

    public CategoryRepository(ApplicationDBContext context) : base(context)
    {
        _context = context;
    }

}
