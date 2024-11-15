using AutoMapper;
using DemoApp.Dto.Categories;
using DemoApp.Dto.Category;
using DemoApp.Dto.Products;
using DemoApp.Models;

namespace DemoApp.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<CreateCategoryDto, Category>();

        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.CategoryName,
                opt => opt.MapFrom(src => src.Category == null ? "WHY NULL" : src.Category.Name));
        CreateMap<ProductDto, Product>();

        CreateMap<CreateProductDto, Product>();
    }
}