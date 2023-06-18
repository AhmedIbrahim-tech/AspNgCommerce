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

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> orderbyDescending { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPaginationEnabled { get; private set; }

    protected void AddIncludes(Expression<Func<T, object>> include) { Includes.Add(include); }
    protected void AddorderbyExpression(Expression<Func<T, object>> orderbyExpression) { this.OrderBy = orderbyExpression; }
    protected void AddorderbyDescendingExpression(Expression<Func<T, object>> orderbyDescendingExpression) { this.orderbyDescending = orderbyDescendingExpression; }

    protected void ApplyPaging(int skip , int take)
    {
        this.Skip = skip;
        this.Take = take;
        this.IsPaginationEnabled = true;
    }
}
