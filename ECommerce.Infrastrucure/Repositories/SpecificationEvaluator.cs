using ECommerce.Core.Interfaces.Specifications;

namespace ECommerce.Infrastrucure.Repositories;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery , ISpecification<TEntity> specification)
    {
        var query = inputquery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }
        query = specification.Includes.Aggregate(query , (current , includes) => current.Include(includes));
        return query;
    }
}
