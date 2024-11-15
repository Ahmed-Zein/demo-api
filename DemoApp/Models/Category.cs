using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DemoApp.Models;

public class Category
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    [JsonIgnore] public ICollection<Product> Products { get; set; } = [];
}