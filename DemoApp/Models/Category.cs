using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DemoApp.Models;

public class Category : BaseModel
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    public List<Product> Products { get; set; } = [];
}