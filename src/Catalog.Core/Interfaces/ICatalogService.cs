using Catalog.Domain.Models;

namespace Catalog.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CatalogItemModel>> GetAllItemsByTypeWithPagging(int pageSize, int pageIndex, int typeId);
        Task<List<CatalogItemModel>> GetAllItems();
        Task<List<CatalogItemModel>> GetAllItemsWithPagging(int pageSize, int pageIndex);
        Task<CatalogItemModel?> FindByIdWithIncludes(Guid id);
    }
}
