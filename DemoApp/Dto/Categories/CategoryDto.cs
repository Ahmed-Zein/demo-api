using System.ComponentModel.DataAnnotations;
using DemoApp.Dto.Products;

namespace DemoApp.Dto.Categories;

public class CategoryDto
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }
}