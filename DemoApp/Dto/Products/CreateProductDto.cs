using System.ComponentModel.DataAnnotations;

namespace DemoApp.Dto.Products;

public class CreateProductDto
{

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; } 
}