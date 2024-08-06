using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.Core.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureURL, o => o.MapFrom<URLResolver>())
            .ReverseMap();

        CreateMap<Identity.Address, AddressDto>().ReverseMap();
        CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
        CreateMap<AddressDto, Entities.OrderAggregate.Address>().ReverseMap();


        CreateMap<Order, OrderToReturnDto>()
     .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
     .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

    }
}
