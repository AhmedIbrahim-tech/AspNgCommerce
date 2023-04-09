namespace ECommerce.Core.Interfaces.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    Task<T> GetById(int id);
    Task Add(T entity);
    Task Edit(T entity);
    Task Delete(int id);
}
