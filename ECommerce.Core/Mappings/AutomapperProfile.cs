namespace ECommerce.Core.Mappings;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ReverseMap();
    }
}
