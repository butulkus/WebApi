using AutoMapper;
using Catalog.DataContext.Entities;
using Catalog.Domain.Models;

namespace Catalog.DataContext.Mapping
{
    public class CatalogItemMapping : Profile
    {
        public CatalogItemMapping()
        {
            CreateMap<CatalogItem, CatalogItemModel>().ReverseMap();
        }
    }
}
