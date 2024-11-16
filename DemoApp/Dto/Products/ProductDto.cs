using System.ComponentModel.DataAnnotations;
using DemoApp.Dto.Categories;
using DemoApp.Models;

namespace DemoApp.Dto.Products;

public class ProductDto
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    public int CategoryId { get; set; }
}

public class CreateProductDto
{
    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }
}

public class UpdateProductDto : CreateProductDto;