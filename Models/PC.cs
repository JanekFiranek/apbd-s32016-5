namespace Apbd5.Models;

public class PC
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public float Weight { get; init; }
    public int Warranty { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Stock { get; init; }

    public ICollection<PCComponent> PCComponents { get; init; } = [];
}