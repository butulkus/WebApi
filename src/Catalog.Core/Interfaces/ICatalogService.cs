using Catalog.Domain.Models;

namespace Catalog.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogItemModel[]> GetAllItemsByTypeWithPagging(int pageSize, int pageIndex, int typeId);
        Task<CatalogItemModel[]> GetAllItems();
        Task<CatalogItemModel[]> GetAllItemsWithPagging(int pageSize, int pageIndex);
        Task<CatalogItemModel?> FindByIdWithIncludes(Guid id);
    }
}
