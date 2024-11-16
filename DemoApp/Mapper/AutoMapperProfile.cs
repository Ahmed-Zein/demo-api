using AutoMapper;
using DemoApp.Dto.AppUser;
using DemoApp.Dto.Categories;
using DemoApp.Dto.Products;
using DemoApp.Models;

namespace DemoApp.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        _registerUser();
        _registerProducts();
        _registerCategories();
    }

    private void _registerUser()
    {
        CreateMap<AppUserDto, AppUser>();
        CreateMap<AppUser, AppUserDto>().ForMember(dest => dest.FullName,
            opt =>
                opt.MapFrom(src => src.FirstName + " " + src.LastName));
        CreateMap<RegisterUserReq, AppUser>();
        CreateMap<LoginRequest, AppUser>();
    }

    private void _registerCategories()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(dto => dto.Products,
                opt => opt.MapFrom(src => src.Products));
        
        CreateMap<CategoryDto, Category>();
        CreateMap<CreateCategoryDto, Category>();
    }

    private void _registerProducts()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductDto, Product>();
    }
}