using AutoMapper;
using Catalog.Api.DataContext.Repositories.Base;
using Catalog.DataContext.Entities;
using Catalog.DataContext.Repositories.RepositoryInterfaces;
using Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataContext.Repositories.ReadRepositories
{
    public class ReadOnlyCatalogItemRepository : ReadOnlyRepositoryBase<CatalogItem, CatalogItemModel>, IReadOnlyCatalogItemRepository
    {
        public ReadOnlyCatalogItemRepository(CatalogDBContext context, IMapper mapper) : base (context, mapper)
        {

        }
    }
}
