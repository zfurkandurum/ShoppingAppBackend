using AutoMapper;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
        
        CreateMap<ProductDto, Product>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CartDto, Cart>();
        CreateMap<CartItemDto, CartItem>();
    }
}