using ECommerce.Core.Interfaces.Specifications;

namespace ECommerce.Infrastrucure.Specifications;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery, ISpecification<TEntity> specification)
    {
        var query = inputquery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.orderbyDescending != null)
        {
            query = query.OrderByDescending(specification.orderbyDescending);
        }

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        query = specification.Includes.Aggregate(query, (current, includes) => current.Include(includes));
        return query;
    }
}
