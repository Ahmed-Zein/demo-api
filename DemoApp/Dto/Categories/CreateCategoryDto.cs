using System.ComponentModel.DataAnnotations;

namespace DemoApp.Dto.Category;

public class CreateCategoryDto
{
    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }
}