using Catalog.Domain.Models;

namespace Catalog.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogItemModel> GetAllItemsByType(int typeId); // think it will be Enum With Types
        Task<CatalogItemModel> GetAllItems();
        Task<CatalogItemModel> GetAllItemsWithPagging(int pageSize, int pageIndex);
    }
}
