using System.Linq.Expressions;

namespace Catalog.Api.DataContext.Repositories.Base
{
    public interface IReadOnlyRepository<TEntity, TModel>
    {
        Task<TModel?> FindByPropertyOrDefault(string propertyName, object propertyValue);
        Task<TModel?> FindByPropertyOrDefaultIncludes(string propertyName, object propertyValue,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TModel[]> FindAll();
        Task<TModel[]> FindAll(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        Task<TModel[]> FindAllWithWherePagging
            (Expression<Func<TEntity, bool>> expression,
            int pageSize,
            int pageIndex,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TModel[]> FindAllWithPagging
            (
            int pageSize,
            int pageIndex,
            params Expression<Func<TEntity, object>>[] includes
            );
    }
}