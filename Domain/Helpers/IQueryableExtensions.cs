using System.Linq.Expressions;

namespace Domain.Helpers
{
    public static class IQueryableExtensions
    {
        public static QueryResult<T> ToQueryResult<T>(this IQueryable<T> dbQuery, int pageNumber = 1, int pageSize = 10, string sourtedBy = "", string sortDirection = "", bool isCountOnly = false) where T : class
        {
            if (dbQuery == null)
                throw new ArgumentNullException("dbQuery");
            int count = dbQuery.Count();
            dbQuery = dbQuery.Page(pageNumber, pageSize, sourtedBy, sortDirection);
            var data = isCountOnly ? null : dbQuery;

            return new QueryResult<T>(data, count, pageNumber, pageSize);
        }
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int pageNumber = 1, int pageSize = 10, string sourtedBy = "", string sortDirection = "")
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize <= 0)
                return source;

            return source.SortBy(sourtedBy, sortDirection).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            // return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName, string sortDir)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return source;

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("Sort property not valid.");
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var methodName = "OrderBy";

            if (sortDir == SortDirection.Descending)
                methodName = "OrderByDescending";

            var resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }


    }
    public static class SortDirection
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";

        public static bool IsAscending(string direction)
        {
            return (string.IsNullOrWhiteSpace(direction))
            || (string.Compare(direction, Ascending, true) == 0);
        }
    }
}
