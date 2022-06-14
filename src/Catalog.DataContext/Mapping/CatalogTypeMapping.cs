using AutoMapper;
using Catalog.DataContext.Entities;
using Catalog.Domain.Models;

namespace Catalog.DataContext.Mapping
{
    public class CatalogTypeMapping : Profile
    {
        public CatalogTypeMapping()
        {
            CreateMap<CatalogType, CatalogTypeModel>().ReverseMap();
        }
    }
}
