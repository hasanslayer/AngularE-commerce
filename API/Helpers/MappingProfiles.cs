using API.Core.Entities;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public static string Lang { get; set; }
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => Lang == "ar" ? src.NameAr : src.NameEn))
                            .ForMember(dst => dst.ProductBrand, opt => opt.MapFrom(src => Lang == "ar" ? src.ProductBrand.NameAr : src.ProductBrand.NameEn))
                            .ForMember(dst => dst.ProductType, opt => opt.MapFrom(src => Lang == "ar" ? src.ProductType.NameAr : src.ProductType.NameEn))
                            .ForMember(dst => dst.ImgUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<ProductBrand, ProductBrandToReturnDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => Lang == "ar" ? src.NameAr : src.NameEn));

            CreateMap<ProductType, ProductTypeToReturnDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => Lang == "ar" ? src.NameAr : src.NameEn));

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CartDto, Cart>();
            CreateMap<CartItemDto, CartItem>();

            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>();
            CreateMap<OrderItem, OrderItemDto>();

        }
    }
}