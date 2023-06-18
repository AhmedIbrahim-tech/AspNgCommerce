using System.Linq.Expressions;

namespace ECommerce.Core.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T,bool>> Criteria { get; }
    List<Expression<Func<T,object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T,object>> orderbyDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPaginationEnabled { get; }

}
