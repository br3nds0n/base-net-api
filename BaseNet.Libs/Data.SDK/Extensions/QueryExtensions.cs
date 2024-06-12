using System.Linq.Expressions;
using BaseNet.Libs.Data.SDK.Query;

namespace BaseNet.Libs.Data.SDK.Base
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> query, QueryOptions<T> options) where T : EntityBase
        {
            if (options is not { Page: not null, Limit: not null }) return query;
            var skip = (options.Page.Value - 1) * options.Limit.Value;
            return query.Skip(skip).Take(options.Limit.Value);
        }

        public static IQueryable<T> Ordenar<T>(this IQueryable<T> query, QueryOrder order,
            Expression<Func<T, object>> orderByExpression)
        {
            return order == QueryOrder.Desc ? query.OrderByDescending(orderByExpression) : query.OrderBy(orderByExpression);
        }
    }
}