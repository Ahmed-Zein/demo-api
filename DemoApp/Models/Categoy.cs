using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models;

//- [ ] Category(Id,Name) with one to many relation.
public class Categoy
{
    public int Id { get; set; }

    [Required, MinLength(4), MaxLength(255)]
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}