using AutoMapper;
using DemoApp.Dto.AppUser;
using DemoApp.Dto.Categories;
using DemoApp.Dto.Category;
using DemoApp.Dto.Products;
using DemoApp.Models;

namespace DemoApp.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        _registerCategories();
        _registerProducts();
        _registerUser();
    }

    private void _registerUser()
    {
        CreateMap<AppUserDto, AppUser>();
        CreateMap<AppUser, AppUserDto>();
        CreateMap<RegisterUserReq, AppUser>();
        CreateMap<LoginRequest, AppUser>();
    }

    private void _registerCategories()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CreateCategoryDto, Category>();
    }

    private void _registerProducts()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.CategoryName,
                opt => opt.MapFrom(src => src.Category == null ? "WHY NULL" : src.Category.Name));
        CreateMap<ProductDto, Product>();

        CreateMap<CreateProductDto, Product>();
    }
}