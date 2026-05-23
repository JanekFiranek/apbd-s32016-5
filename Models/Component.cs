namespace Apbd5.Models;

public class Component
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public int ComponentManufacturerId { get; init; }
    public int ComponentTypeId { get; init; }

    public ComponentManufacturer Manufacturer { get; init; } = null!;
    public ComponentType Type { get; init; } = null!;
    public ICollection<PCComponent> PCComponents { get; init; } = [];
}