namespace ECommerce.Core.Interfaces.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> GetEntityWithSpec(ISpecification<T> specification);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
    //Task AddAsync(T entity);
    //Task EditAsync(T entity); 
    //Task DeleteAsync(int id);
}
