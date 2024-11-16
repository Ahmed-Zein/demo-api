namespace DemoApp.Models;

public abstract class BaseModel
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}