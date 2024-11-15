using System.ComponentModel.DataAnnotations;
using DemoApp.Dto.Categories;

namespace DemoApp.Dto.Products;

public class ProductDto
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }
    
    public string CategoryName { get; set; }
}