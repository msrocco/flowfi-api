using System.Linq.Expressions;

namespace FlowFi.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}
