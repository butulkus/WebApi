using AutoMapper;
using Catalog.DataContext.Entities;
using Catalog.DataContext.Repositories.Base;
using Catalog.DataContext.Repositories.RepositoryInterfaces;
using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.DataContext.Repositories.WriteRepositories
{
    public class WriteCatalogItemRepository : WriteRepository<CatalogItem, CatalogItemModel>, IWriteCatalogItemRepository
    {
        public WriteCatalogItemRepository(ILogger logger, CatalogDBContext catalogDBContext, IMapper mapper) : base(logger, catalogDBContext, mapper)
        {
        }

        public async Task<int> UpdateItemPrice(Guid itemId, decimal newPrice)
        {
            var entity = await _catalogDBContext.CatalogItem
                .SingleOrDefaultAsync(x => x.ItemId == itemId);

            if (entity == null)
                throw new NullReferenceException("entity was a null");

            entity.Price = newPrice;
            var fieldChangedCount = await _catalogDBContext.SaveChangesAsync();

            _logger.LogInformation("Updating model in DB. Model - {model}, field changed {field}", entity.GetType().Name, fieldChangedCount);

            return fieldChangedCount;
        }
    }
}
