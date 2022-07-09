namespace Catalog.DataContext.Repositories.RepositoryInterfaces
{
    public interface IWriteCatalogItemRepository
    {
        Task<int> UpdateItemPrice(Guid itmeId, decimal newPrice);
    }
}
