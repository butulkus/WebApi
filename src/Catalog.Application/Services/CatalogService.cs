﻿using Catalog.Core.Interfaces;
using Catalog.DataContext.Entities;
using Catalog.DataContext.Repositories.RepositoryInterfaces;
using Catalog.Domain.Models;

namespace Catalog.Application.Services
{
    public class CatalogService : ICatalogService
    {
        public readonly IReadOnlyCatalogItemRepository _catalogReadOnlyRepository;

        public CatalogService(IReadOnlyCatalogItemRepository catalogItemRepository)
        {
            _catalogReadOnlyRepository = catalogItemRepository;
        }

        public async Task<CatalogItemModel?> FindByIdWithIncludes(Guid id)
        {
            var item = await _catalogReadOnlyRepository.FindByPropertyOrDefaultIncludes(nameof(CatalogItem.ItemId),
                id,
                s => s.CatalogBrand,
                s => s.CatalogType
                );

            return item;
        }

        public async Task<List<CatalogItemModel>> GetAllItems()
        {
            var items = await _catalogReadOnlyRepository.FindAll();

            return items;
        }

        public async Task<List<CatalogItemModel>> GetAllItemsByTypeWithPagging(int pageSize, int pageIndex, int typeId)
        {
            var items = await _catalogReadOnlyRepository.FindAllWithIncludesPagging(u => u.CatalogTypeId == typeId,
                pageSize,
                pageIndex,
                u => u.CatalogBrand,
                u => u.CatalogType);

            return items;
        }

        public async Task<List<CatalogItemModel>> GetAllItemsWithPagging(int pageSize, int pageIndex)
        {
            var items = await _catalogReadOnlyRepository.FindAllPagging(pageSize,
                pageIndex,
                u => u.CatalogBrand,
                u => u.CatalogType
                );

            return items;
        }
    }
}
