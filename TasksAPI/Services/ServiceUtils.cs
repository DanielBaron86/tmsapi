using System.Linq.Expressions;

namespace TasksAPI.Services;

public class ServiceUtils
{
    public static IQueryable<T> CreateFilter<T>(IQueryable<T> query, string propertyName, string searchTerm)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, propertyName);
        object value = searchTerm;
        if (property.Type != typeof(string))
            value = Convert.ChangeType(value, property.Type);
        if (property.Type != typeof(string))
        {
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    property,
                    Expression.Constant(value)
                ),
                parameter
            );
            return query.Where(filterLambda);
        }
        else
        {
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    property,
                    typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) }),
                    Expression.Constant(value)
                ),
                parameter
            );
            return query.Where(filterLambda);
        }
    }

    public static Expression<Func<T, bool>> CreateFilterForPredicateBuilder<T>(string propertyName, string searchTerm)
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, propertyName);
        object value = searchTerm;
        if (property.Type != typeof(string))
            value = Convert.ChangeType(value, property.Type);
        if (property.Type != typeof(string))
        {
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    property,
                    Expression.Constant(value)
                ),
                parameter
            );
            return filterLambda;
        }
        else
        {
            var filterLambda = Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    property,
                    typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) }),
                    Expression.Constant(value)
                ),
                parameter
            );
            return filterLambda;
        }
    }
}