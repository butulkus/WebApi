using Catalog.Api.DataContext.Repositories.Base;
using Catalog.DataContext.Entities;
using Catalog.Domain.Models;

namespace Catalog.DataContext.Repositories.RepositoryInterfaces
{
    public interface IReadOnlyCatalogItemRepository : IReadOnlyRepository<CatalogItem, CatalogItemModel>
    {
    }
}
