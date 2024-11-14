using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models;

// - [ ] Product (Id,Name,CategoryId,etc.)
public class Product
{
    [Key] public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    [Required] public int CategoryId { get; set; }
    public Categoy Category { get; set; } // navigational
}