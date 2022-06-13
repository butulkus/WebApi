using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog.Api.DataContext.Extensions
{
    public static class QueribleExtensions
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(
            this IQueryable<TEntity> query,
            params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class
        {
            if (includes is not null)
                query = includes.Aggregate(query, (current, next) => current.Include(next));

            return query;
        }
    }
}