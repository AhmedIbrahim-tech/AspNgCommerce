using System.Linq.Expressions;

namespace ECommerce.Core.Interfaces.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
        
    }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    protected void AddIncludes(Expression<Func<T, object>> include) { Includes.Add(include); }
}
