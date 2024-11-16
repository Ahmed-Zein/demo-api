using System.ComponentModel.DataAnnotations;
using DemoApp.Dto.Products;
using DemoApp.Models;

namespace DemoApp.Dto.Categories;

public class CategoryDto
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    public List<ProductDto> Products { get; set; } = [];
}

public class CreateCategoryDto
{
    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }
}

public class UpdateCategoryDto : CreateCategoryDto;