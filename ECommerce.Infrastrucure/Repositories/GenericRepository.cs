using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDBContext _context;

    public GenericRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
    {
        return await AppSpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
        return await AppSpecification(specification).ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await AppSpecification(specification).CountAsync();
    }

    private IQueryable<T> AppSpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }



    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}