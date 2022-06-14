using AutoMapper;
using Catalog.DataContext.Entities;
using Catalog.Domain.Models;

namespace Catalog.DataContext.Mapping
{
    public class CatalogBrandMapping : Profile
    {
        public CatalogBrandMapping()
        {
            CreateMap<CatalogBrand, CatalogBrandModel>().ReverseMap();
        }
    }
}
