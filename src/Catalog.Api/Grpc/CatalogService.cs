using Catalog.DataContext.Entities;
using Catalog.DataContext.Repositories.RepositoryInterfaces;
using Catalog.Domain.Models;
using Grpc.Core;
using GrpcServer;

namespace Catalog.Api.Grpc
{
    public class CatalogService : GrpcServer.Catalog.CatalogBase
    {
        public readonly IReadOnlyCatalogItemRepository _catalogReadOnlyRepository;
        public CatalogService(IReadOnlyCatalogItemRepository catalogItemRepository)
        {
            _catalogReadOnlyRepository = catalogItemRepository;
        }

        public override async Task<CatalogItemResponse> GetItemById(CatalogIdRequest id, ServerCallContext context)
        {
            var item = await _catalogReadOnlyRepository.FindByPropertyOrDefaultIncludes(nameof(CatalogItem.ItemId),
                new Guid(id.ItemId),
                s => s.CatalogBrand,
                s => s.CatalogType
                );

            var brand = new GrpcServer.CatalogBrand
            {
                Id = item.CatalogBrand.Id.ToString(),
                Name = item.CatalogBrand.Brand
            };
            var type = new GrpcServer.CatalogType
            {
                Id = item.CatalogType.Id.ToString(),
                Type = item.CatalogType.Type
            };

            return new CatalogItemResponse
            {
                Name = item.Name,
                ItemHashCode = item.ItemHashCode,
                Description = item.Description,
                Price = (double)item.Price,
                CatalogBrandId = item.CatalogBrandId,
                CatalogTypeId = item.CatalogTypeId,
                CatalogBrand = brand,
                CatalogType = type
            };
        }

        public override async Task<CatalogItemListResponse> GetItemsListWithPagging(CatalogWholeItemsRequest request, ServerCallContext context)
        {
            var items = await _catalogReadOnlyRepository.FindAllWithPagging(request.PageSize, request.PageIndex);

            var itemsList = new CatalogItemListResponse();

            items.ToList().ForEach(i =>
            {
                itemsList.Data.Add(new CatalogItemResponse()
                {
                    Name = i.Name,
                    ItemHashCode = i.ItemHashCode,
                    Description = i.Description,
                    Price = (double)i.Price,
                    CatalogBrandId = i.CatalogBrandId,
                    CatalogTypeId = i.CatalogTypeId,
                });
            });

            return itemsList;
        }
    }
}
