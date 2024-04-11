using AutoMapper;
using ShoppingAppBackend.Dto;
using ShoppingAppBackend.Models;

namespace ShoppingAppBackend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<ApplicationUser, RegisterDto>();
        CreateMap<ApplicationUser, LoginDto>();
        
        CreateMap<CartDto, Cart>();
        CreateMap<CartItemDto, CartItem>();
        CreateMap<CategoryDto, Category>();
        CreateMap<OrderDto, Order>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<ProductDto, Product>();
        CreateMap<RegisterDto, ApplicationUser>();
        CreateMap<LoginDto, ApplicationUser>();
    }
}